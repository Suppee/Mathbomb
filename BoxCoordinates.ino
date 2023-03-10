#include <Wire.h>
#include <Arduino_LSM6DS3.h>
#include <WiFiNINA.h>

char ssid[] = "Waoo4920_7388";             //  your network SSID (name) between the " "
char pass[] = "nnmt8333";      // your network password between the " "
int keyIndex = 0;                 // your network key Index number (needed only for WEP)
int status = WL_IDLE_STATUS;      //connection status
WiFiServer server(80);            //server socket

WiFiClient client = server.available();

float roll, pitch, yaw;
float gyro_roll, gyro_pitch, gyro_yaw;
float gyro_x, gyro_y, gyro_z;

void setup() {
  Serial.begin(9600);
  pinMode(LED_BUILTIN, OUTPUT);
  while (!Serial);
  
  enable_WiFi();
  connect_WiFi();

  server.begin();
  printWifiStatus();

  if (!IMU.begin()) {
    Serial.println("Failed to initialize IMU!");
    while (1);
  }
}

void loop() {
  client = server.available();

  if (client) {
    printWEB();
  }
}

void printWifiStatus() {
  // print the SSID of the network you're attached to:
  Serial.print("SSID: ");
  Serial.println(WiFi.SSID());

  // print your board's IP address:
  IPAddress ip = WiFi.localIP();
  Serial.print("IP Address: ");
  Serial.println(ip);

  // print the received signal strength:
  long rssi = WiFi.RSSI();
  Serial.print("signal strength (RSSI):");
  Serial.print(rssi);
  Serial.println(" dBm");

  Serial.print("To see this page in action, open a browser to http://");
  Serial.println(ip);
  digitalWrite(LED_BUILTIN, HIGH);   // turn the LED on (HIGH is the voltage level)
  delay(1000);                       // wait for a second
  digitalWrite(LED_BUILTIN, LOW);    // turn the LED off by making the voltage LOW  
}

void enable_WiFi() {
  // check for the WiFi module:
  if (WiFi.status() == WL_NO_MODULE) {
    Serial.println("Communication with WiFi module failed!");
    // don't continue
    while (true);
  }

  String fv = WiFi.firmwareVersion();
  if (fv < "1.0.0") {
    Serial.println("Please upgrade the firmware");
  }
}

void connect_WiFi() {
  // attempt to connect to Wifi network:
  while (status != WL_CONNECTED) {
    Serial.print("Attempting to connect to SSID: ");
    Serial.println(ssid);
    // Connect to WPA/WPA2 network. Change this line if using open or WEP network:
    status = WiFi.begin(ssid, pass);

    // wait 10 seconds for connection:
    delay(10000);
  }
}

void printWEB() {

  if (client) {                             // if you get a client,
    Serial.println("new client");           // print a message out the serial port
    String currentLine = "";                // make a String to hold incoming data from the client
    while (client.connected()) {            // loop while the client's connected
      if (client.available()) {             // if there's bytes to read from the client,
        char c = client.read();             // read a byte, then
        Serial.write(c);                    // print it out the serial monitor
        if (c == '\n') {                    // if the byte is a newline character

          // if the current line is blank, you got two newline characters in a row.
          // that's the end of the client HTTP request, so send a response:
          if (currentLine.length() == 0) {

            // HTTP headers always start with a response code (e.g. HTTP/1.1 200 OK)
            // and a content-type so the client knows what's coming, then a blank line:
            client.println("HTTP/1.1 200 OK");
            client.println("Content-type:text/html");
            client.println();

            if (IMU.accelerationAvailable() && IMU.gyroscopeAvailable()) {
              IMU.readAcceleration(roll, pitch, yaw);
              IMU.readGyroscope(gyro_x, gyro_y, gyro_z);
          
              gyro_roll = roll + gyro_x;
              gyro_pitch = pitch + gyro_y;
              gyro_yaw = yaw + gyro_z;
          
              // Calculate the quaternion from the roll, pitch, and yaw angles
              float cy = cos(yaw * 0.5);
              float sy = sin(yaw * 0.5);
              float cp = cos(pitch * 0.5);
              float sp = sin(pitch * 0.5);
              float cr = cos(roll * 0.5);
              float sr = sin(roll * 0.5);
          
              float w = cy * cp * cr + sy * sp * sr;
              float x = cy * cp * sr - sy * sp * cr;
              float y = sy * cp * sr + cy * sp * cr;
              float z = sy * cp * cr - cy * sp * sr;
          
              /*Serial.print("Roll: ");
              Serial.print(roll);
              Serial.print("\tGyro Roll: ");
              Serial.println(gyro_roll);
          
              Serial.print("Pitch: ");
              Serial.print(pitch);
              Serial.print("\tGyro Pitch: ");
              Serial.println(gyro_pitch);
          
              Serial.print("Yaw: ");
              Serial.print(yaw);
              Serial.print("\tGyro Yaw: ");
              Serial.println(gyro_yaw);*/
          
              // Print the quaternion
              //Serial.print("Quaternion: ");
              client.print(w);
              client.print(",");
              client.print(x);
              client.print(",");
              client.print(y);
              client.print(",");
              client.println(z);
            }
                      
           
            // The HTTP response ends with another blank line:
            //client.println();
            // break out of the while loop:
            break;
          }
          else {      // if you got a newline, then clear currentLine:
            currentLine = "";
          }
        }
        else if (c != '\r') {    // if you got anything else but a carriage return character,
          currentLine += c;      // add it to the end of the currentLine
        }
      }
    }
    // close the connection:
    client.stop();
    Serial.println("client disconnected");
  }
}
