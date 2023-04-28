from sense_hat import SenseHat
import requests
import serial
from time import sleep

# initialization
sense = SenseHat()
sense.clear() #clear LCD screen

# configure serial communication
ser = serial.Serial('/dev/ttyACM0', 115200)

# ThingSpeak API key for test project channel
myAPI = 'Z1VLP2N2B0GCYN01' 

# URL to change the data
URL = 'https://api.thingspeak.com/update.json'

def get_sound_and_light_values():
    soundValues = []
    lightValues = []
    # Read 20 values from Arduino and store them in an array for each sound and light sensor
    for i in range(20):
        line = ser.readline().strip().decode('utf-8')
        print("getting arduino data",i)
        values = line.split(",")
        # check if there are three distinct values
        if len(values) != 3:
            print("Invalid data format, trying again...")
            # recursively call the function to get valid data
            return get_sound_and_light_values()
        soundValue = int(values[0]) # get sound values from index 0
        soundValues.append(soundValue) # append sound values to the soundValues array
        lightValue = int(values[2])
        lightValues.append(lightValue) # append light values to the lightValues array
    return soundValues, lightValues, values

def calculate_noise_level(soundValues):
    # Calculate noise level based on the differences between sound values
    noise = 0
    for i in range(1, len(soundValues)):
        diff = abs(soundValues[i] - soundValues[i-1])
        noise += diff
    noise /= len(soundValues) - 1
    return noise

def calculate_light_disturbance_level(lightValues):
    # Calculate light disturbance level based on the differences between light values
    lightDisturbance = 0
    for i in range(1, len(lightValues)):
        diff = abs(lightValues[i] - lightValues[i-1])
        lightDisturbance += diff
    lightDisturbance /= len(lightValues) - 1
    return lightDisturbance

def sensehat_data():
    # Reading from SenseHat and storing the temperature, humidity & pressure
    tempValues = []
    humidityValues = []
    pressureValues = []
    print("getting senseHAT data")
    # Read 20 values from SenseHat and store them in an array
    for i in range(20):
        tempValues.append(sense.get_temperature())
        humidityValues.append(sense.get_humidity())
        pressureValues.append(sense.get_pressure())
        sleep(0.1) # wait for 0.5 seconds before taking the next value
        
    #calculate average of each sensor
    temp = sum(tempValues)/len(tempValues)
    humid = sum(humidityValues)/len(humidityValues) 
    press = sum(pressureValues)/len(pressureValues) 
    return temp, humid, press

def arduino_data():
    # read sensor values from Arduino
    print("getting arduino data")
    soundValues, lightValues, values = get_sound_and_light_values() # also returning values as the airQuality is dependent on value index 1
    noise = calculate_noise_level(soundValues)
    lightDisturbance = calculate_light_disturbance_level(lightValues)
        
    # Set lightDisturbanceStatus to 0 or 1 based on the lightDisturbance value
    lightDisturbanceStatus = 0 if lightDisturbance <= 10 else 1
    
    # Set noiseStatus to 0 or 1 based on the noise value
    noiseStatus = 0 if noise <= 5 else 1
    
    unAdjustedAirQuality = int(values[1]) # get the air quality value at index 1
    print(unAdjustedAirQuality)
    sound = int(values[0]) # get the sound value at index 0
    airQuality = (5/(unAdjustedAirQuality* (5.0/1023.0))-1)*30 # adjust the airQuality value to 
    light = int(values[2]) # inclued the most recent light luminence to reflect brightness of room
    
    return sound, noiseStatus, airQuality, light, lightDisturbanceStatus

while True:
    
    temp, humid, press = sensehat_data()
    sound, noiseStatus, airQuality, light, lightDisturbanceStatus = arduino_data()
    print("noise level", sound)
    # Formatting to two decimal places for display
    print("Temperature = {:.2f}, Humidity = {:.2f}, Pressure = {:.2f}".format(temp,humid,press))
    # print sensor values from Arduimp
    print("Air Quality = ", airQuality)
    print("Light = ", light)
    print("Light disturbance status = ", lightDisturbanceStatus)
    print("Noise status = ", noiseStatus)
    # If reading is valid
    if isinstance(temp, float) and isinstance(humid, float) and isinstance(press, float):
        
        jsonData = '{"api_key":"%s","field1":%f,"field2":%f,"field3":%f,"field4":%f,"field5":%f,"field6":%f,"field7":%f,"field8":%f}' % (myAPI, temp, humid, press, sound, airQuality, light, lightDisturbanceStatus, noiseStatus)
        print(jsonData)

        header = {"Content-Type": "application/json","Accept": "text/plain"} 
        
        try:
            # Sending the data to thingspeak
            response = requests.post(url=URL, data = jsonData, headers = header)
            print(response)
    
        except Exception as e:
            print('Break..')
            print(e)
            break
    else:
        print('Error')
        break
    
    sleep(2)

