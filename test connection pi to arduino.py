import serial

ser = serial.Serial('/dev/ttyACM1', 9600)
while True:
    line = ser.readline().decode('utf-8').rstrip()
    print(line)
