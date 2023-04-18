import time
import network
import socket
import machine
import ustruct
from bno055 import *
from neopixel import Neopixel

ssid = 'Birthe Kristensens iPhone'
password = '2206Ark95'

UDP_IP = "172.20.10.3"
UDP_PORT = 3001
UDP_BUFF = 256
# 
i2c1 = machine.SoftI2C(sda=machine.Pin(0), scl=machine.Pin(1), timeout=100_000)  # EIO error almost immediately
i2c2 = machine.SoftI2C(sda=machine.Pin(2), scl=machine.Pin(3), timeout=100_000)  # EIO error almost immediately
imu1 = BNO055(i2c1)
imu2 = BNO055(i2c2)

qw = 0
qx = 0
qy = 0
qz = 0

lx = 0
ly = 0
lz = 0

pixel = Neopixel(1, 0, 15, "GRB")

green = (0, 255, 0)
yellow = (200, 150, 0)
orange = (200, 50, 0)
red = (255, 0, 0)
black = (0,0,0)

pixel.brightness(100)
pixel.fill(black)
pixel.show()

wlan = network.WLAN(network.STA_IF)
wlan.active(True)
wlan.connect(ssid, password)

# Wait for connect or fail
max_wait = 10
while max_wait > 0:
    if wlan.status() < 0 or wlan.status() >= 3:
        break
    max_wait -= 1
    print('waiting for connection...')
    time.sleep(1)

# Handle connection error
if wlan.status() != 3:
    raise RuntimeError('network connection failed')
else:
    status = wlan.ifconfig()
    print( 'Connected to ' + ssid + '. ' + 'Device IP: ' + status[0] )    

sock = socket.socket(socket.AF_INET, socket.SOCK_DGRAM)
sock.bind((status[0], 3002))

def setCalInfo():
    cal1 = imu1.cal_status()
    cal2 = imu2.cal_status()
    
    calibration = cal1[1] + cal2[1] + cal1[2] + cal2[2] + cal1[3] + cal2[3] 
    
    if calibration < 6:
        #print(cal1[0], cal1[1], cal1[2], cal1[3], cal2[0], cal2[1], cal2[2], cal2[3])
        pixel.fill(red)
        pixel.show()
        return False
    
    if calibration < 12:
        #print(cal1[0], cal1[1], cal1[2], cal1[3], cal2[0], cal2[1], cal2[2], cal2[3])
        pixel.fill(orange)
        pixel.show()
        return False
    
    if calibration < 18:
        #print(cal1[0], cal1[1], cal1[2], cal1[3], cal2[0], cal2[1], cal2[2], cal2[3])
        pixel.fill(yellow)
        pixel.show()
        return False
    
    pixel.fill(green)
    pixel.show()
    return True
    

def getData():
    quat1 = imu1.quaternion()
    quat2 = imu2.quaternion()
    linAcc1 = imu1.lin_acc()
    linAcc2 = imu2.lin_acc()
    
    qw = quat1[0] + quat2[0]
    if qw != 0:
        qw/2
        
    qx = quat1[1] + quat2[1]
    if qx != 0:
        qx/2
    
    qy = quat1[2] + quat2[2]
    if qy != 0:
        qy/2
        
    qz = quat1[3] + quat2[3]
    if qz != 0:
        qz/2
    
    lx = linAcc1[0] + linAcc2[0]
    if lx != 0:
        lx/2
    
    ly = linAcc1[1] + linAcc2[1]
    if ly != 0:
        ly/2
        
    lz = linAcc1[2] + linAcc2[2]
    if lz != 0:
        lz/2
        
    return qw, qx, qy, qz, lx, ly, lz
       
while True:
    time.sleep(0.1)
    try:
        if setCalInfo() == True:
            msgSend = str(getData())
            sock.sendto(msgSend.encode(), (UDP_IP, UDP_PORT))
            #print ("message sent")
            #size, addr = sock.recvfrom(UDP_BUFF)
            #msgRec = size.decode('utf-8')  # assume a string, so convert from bytearray
            #print(f"Received message from {addr[0]}:", msgRec)
    except:
        print ("Network issue")
        
        

