/*#include <Wire.h>
#include <Adafruit_Sensor.h>
#include <Adafruit_BNO055.h>
#include <utility/imumaths.h>
#include <math.h>

#define BNO055_SAMPLERATE_DELAY_MS (100)
Adafruit_BNO055 bno = Adafruit_BNO055();

// Variables for storing the orientation quaternion
float q0, q1, q2, q3;

// Variables for storing the vector positoin
float p0, p1, p2;

void setupIMU() {
  bno.begin();
  bno.setExtCrystalUse(true);

  // Calibrate the BNO055
  while (!bno.isFullyCalibrated()) {
    uint8_t system, gyro, accel, mg = 0; 
    bno.getCalibration(&system, &gyro, &accel, &mg);
    Serial.print(system);
    Serial.print(gyro);
    Serial.print(accel);
    Serial.println(mg);
    delay(1000); // Wait 1 second for calibration
  }
  Serial.println("BNO055 fully calibrated!");
}

void getPosNRot() {
  // Quaternion for rotation
  imu::Quaternion quat = bno.getQuat();

  // Vector3 for position
  imu::Vector<3> pos = bno.getVector(Adafruit_BNO055::VECTOR_EULER);

  // Output data
  q0 = quat.w();
  q1 = quat.x();
  q2 = quat.y();
  q3 = quat.z();

  p0 = pos.x();
  p1 = pos.y();
  p2 = pos.z();
  delay(BNO055_SAMPLERATE_DELAY_MS);
}

void getOrientation(float* orientation) {
  orientation[0] = q0;
  orientation[1] = q1;
  orientation[2] = q2;
  orientation[3] = q3;
}

void getPosition(float* pos) {
  pos[0] = p0;
  pos[1] = p1;
  pos[2] = p2;
}*/

#include <Wire.h>
#include <Adafruit_Sensor.h>
#include <Adafruit_BNO055.h>
#include <utility/imumaths.h>

/* This driver uses the Adafruit unified sensor library (Adafruit_Sensor),
   which provides a common 'type' for sensor data and some helper functions.

   To use this driver you will also need to download the Adafruit_Sensor
   library and include it in your libraries folder.

   You should also assign a unique ID to this sensor for use with
   the Adafruit Sensor API so that you can identify this particular
   sensor in any data logs, etc.  To assign a unique ID, simply
   provide an appropriate value in the constructor below (12345
   is used by default in this example).

   Connections
   ===========
   Connect SCL to analog 5
   Connect SDA to analog 4
   Connect VDD to 3.3-5V DC
   Connect GROUND to common ground

   History
   =======
   2015/MAR/03  - First release (KTOWN)
*/

/* Set the delay between fresh samples */
#define BNO055_SAMPLERATE_DELAY_MS (100)

// Check I2C device address and correct line below (by default address is 0x29 or 0x28)
//                                   id, address
Adafruit_BNO055 bno = Adafruit_BNO055(55, 0x28);
uint8_t sys, gyro, accel, mag = 0;
float r0, r1, r2;

/**************************************************************************/
/*
    Displays some basic information on this sensor from the unified
    sensor API sensor_t type (see Adafruit_Sensor for more information)
*/
/**************************************************************************/
void displaySensorDetails(void)
{
  sensor_t sensor;
  bno.getSensor(&sensor);
  Serial.println("------------------------------------");
  Serial.print  ("Sensor:       "); Serial.println(sensor.name);
  Serial.print  ("Driver Ver:   "); Serial.println(sensor.version);
  Serial.print  ("Unique ID:    "); Serial.println(sensor.sensor_id);
  Serial.print  ("Max Value:    "); Serial.print(sensor.max_value); Serial.println(" xxx");
  Serial.print  ("Min Value:    "); Serial.print(sensor.min_value); Serial.println(" xxx");
  Serial.print  ("Resolution:   "); Serial.print(sensor.resolution); Serial.println(" xxx");
  Serial.println("------------------------------------");
  Serial.println("");
  delay(500);
}

/**************************************************************************/
/*
    Arduino setup function (automatically called at startup)
*/
/**************************************************************************/
void setupIMU(void)
{
  //Serial.begin(115200);
  //Serial.println("Orientation Sensor Test"); Serial.println("");

  /* Initialise the sensor */
  if(!bno.begin())
  {
    /* There was a problem detecting the BNO055 ... check your connections */
    Serial.print("Ooops, no BNO055 detected ... Check your wiring or I2C ADDR!");
    while(1);
  }
   
  delay(1000);

  /* Use external crystal for better accuracy */
  bno.setExtCrystalUse(true);
   
  /* Display some basic information on this sensor */
  displaySensorDetails();
}

/**************************************************************************/
/*
    Arduino loop function, called once 'setup' is complete (your own code
    should go here)
*/
/**************************************************************************/
void UpdateIMU(void)
{
  /* Get a new sensor event */
  sensors_event_t event;
  bno.getEvent(&event);

  /* Board layout:
         +----------+
         |         *| RST   PITCH  ROLL  HEADING
     ADR |*        *| SCL
     INT |*        *| SDA     ^            /->
     PS1 |*        *| GND     |            |
     PS0 |*        *| 3VO     Y    Z-->    \-X
         |         *| VIN
         +----------+
  */

  /* The processing sketch expects data as roll, pitch, heading */
  /*Serial.print(F("Orientation: "));
  Serial.print((float)event.orientation.x);
  Serial.print(F(" "));
  Serial.print((float)event.orientation.y);
  Serial.print(F(" "));
  Serial.print((float)event.orientation.z);
  Serial.println(F(""));*/
  r0 = (float)event.orientation.x;
  r1 = (float)event.orientation.y;
  r2 = (float)event.orientation.z;

  /* Also send calibration data for each sensor. */
  //uint8_t sys, gyro, accel, mag = 0;
  bno.getCalibration(&sys, &gyro, &accel, &mag);
  Serial.print(F("Calibration: "));
  Serial.print(sys, DEC);
  Serial.print(F(" "));
  Serial.print(gyro, DEC);
  Serial.print(F(" "));
  Serial.print(accel, DEC);
  Serial.print(F(" "));
  Serial.println(mag, DEC);

  delay(BNO055_SAMPLERATE_DELAY_MS);
}

void getOrientation(float* orientation) {
  orientation[0] = r0;
  orientation[1] = r1;
  orientation[2] = r2;
}

void getCalibration(uint8_t* calibration) {
  calibration[0] = sys;
  calibration[1] = gyro;
  calibration[2] = accel;
  calibration[3] = mag;
}
