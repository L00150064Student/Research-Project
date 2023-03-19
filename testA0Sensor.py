import serial

# configure serial communication
ser = serial.Serial('/dev/ttyACM1', 9600)

while True:
    # read sensor values from Arduino
    line = ser.readline().strip().decode('utf-8')
    print(line)
    values = line.split(",")
    sensor1 = int(values[0])
    sensor2 = int(values[1])
    sensor3 = int(values[2])
    
    # print sensor values
    print("Sensor 1:", sensor1)
    print("Sensor 2:", sensor2)
    print("Sensor 3:", sensor3)
