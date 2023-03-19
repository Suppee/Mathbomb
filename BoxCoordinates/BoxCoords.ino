#include <Adafruit_BNO055.h>
#include <Wire.h>

Adafruit_BNO055 bno = Adafruit_BNO055();

void setup() {
  Serial.begin(9600);
  Serial.println("BNO055 test");
  if (!bno.begin()) {
    Serial.println("Ooops, no BNO055 detected ... Check your wiring or I2C ADDR!");
    while (1);
  }
  bno.setExtCrystalUse(true);

  // Calibrate the BNO055
  while (!bno.isFullyCalibrated()) {
    Serial.println("BNO055 not fully calibrated, please calibrate sensor!");
    delay(1000); // Wait 1 second for calibration
  }
  Serial.println("BNO055 fully calibrated!");
}

void loop() {
  sensors_event_t event;
  bno.getEvent(&event);

  // Quaternion for rotation
  imu::Quaternion quat = bno.getQuat();

  // Vector3 for position
  imu::Vector<3> position = bno.getVector(Adafruit_BNO055::VECTOR_EULER);

  // Output data in a format that can be easily parsed by Unity
  Serial.print("Quaternion: ");
  Serial.print(quat.x(), 4);
  Serial.print(",");
  Serial.print(quat.y(), 4);
  Serial.print(",");
  Serial.print(quat.z(), 4);
  Serial.print(",");
  Serial.print(quat.w(), 4);
  Serial.print(";");

  Serial.print("Position: ");
  Serial.print(position.x(), 4);
  Serial.print(",");
  Serial.print(position.y(), 4);
  Serial.print(",");
  Serial.print(position.z(), 4);
  Serial.println(";");

  delay(50);
}
