import time
import network
import socket
from machine import Pin

ssid = 'Birthe Kristensens iPhone'
password = '2206Ark95'

UDP_IP = "172.20.10.3"
UDP_PORT = 3001
UDP_BUFF = 256

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
sock.bind((status[0], 3003))

col_list=[2,3,4,5]
row_list=[6,7,8,9]

for x in range(0,4):
    row_list[x]=Pin(row_list[x], Pin.OUT)
    row_list[x].value(1)


for x in range(0,4):
    col_list[x] = Pin(col_list[x], Pin.IN, Pin.PULL_UP)
    
key_map=[["D","#","0","*"],\
        ["C","9","8","7"],\
        ["B","6","5","4"],\
        ["A","3","2","1"]]

def Keypad4x4Read(cols,rows):
  for r in rows:
    r.value(0)
    result=[cols[0].value(),cols[1].value(),cols[2].value(),cols[3].value()]
    if min(result)==0:
      key=key_map[int(rows.index(r))][int(result.index(0))]
      r.value(1) # manages key keept pressed
      return(key)
    r.value(1)


while True:
    time.sleep(0.3)
    try:
        key=Keypad4x4Read(col_list, row_list)
        if key != None:
            msgSend = str(key)
            sock.sendto(msgSend.encode(), (UDP_IP, UDP_PORT))
    except:
        print ("Network issue")


