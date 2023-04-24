from sense_hat import SenseHat
import requests
import serial
from time import sleep

# initialization
sense = SenseHat()
sense.clear() #clear LCD screen

# configure serial communication
ser = serial.Serial('/dev/ttyACM1', 9600)

# ThingSpeak API key for test project channel
myAPI = 'Z1VLP2N2B0GCYN01' 

# URL to change the data
URL = 'https://api.thingspeak.com/update.json'

def sensehat_data():
    # Reading from SenseHat and storing the temperature, humidity & pressure
    temp = sense.get_temperature()
    print("getting senseHAT data")
    humid = sense.get_humidity() 
    press = sense.get_pressure() 
    return temp, humid, press

def arduino_data():
    # read sensor values from Arduino
    line = ser.readline().strip().decode('utf-8')
    print("getting arduino data")
    values = line.split(",")
    sound = int(values[0])
    airQuality = int(values[1])
    light = int(values[2])
    
    return sound, airQuality, light

while True:
    
    temp, humid, press = sensehat_data()
    sound, airQuality, light = arduino_data()
    # Formatting to two decimal places for display
    print("Temperature = {:.2f}, Humidity = {:.2f}, Pressure = {:.2f}".format(temp,humid,press))
    # print sensor values from Arduimp
    print("Sound = ", sound)
    print("Air Quality = ", airQuality)
    print("Light = ", light)
    # If reading is valid
    if isinstance(temp, float) and isinstance(humid, float) and isinstance(press, float):
        
        jsonData = '{"api_key":"%s","field1":%f,"field2":%f,"field3":%f,"field4":%f,"field5":%f,"field6":%f}' % (myAPI, temp, humid, press, sound, airQuality, light)
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
    
    sleep(10)