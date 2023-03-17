#include <Arduino_LSM6DS3.h>

#define NUM_READINGS 200
#define CALIBRATION_DELAY_MS 1000

// Variables for storing the orientation quaternion
float q0, q1, q2, q3;

// Constants for the complementary filter
const float alpha = 0.96; // Weight for gyroscope data
const float beta = 0.04;  // Weight for accelerometer data

float previousTime;

float ax_offset = 0.0;
float ay_offset = 0.0;
float az_offset = 0.0;
float gx_offset = 0.0;
float gy_offset = 0.0;
float gz_offset = 0.0;

// Function to update the orientation quaternion
void updateOrientation()
{
  if(IMU.accelerationAvailable() && IMU.gyroscopeAvailable()) {
    float ax, ay, az, gx, gy, gz;
    IMU.readAcceleration(ax, ay, az);
    IMU.readGyroscope(gx, gy, gz);

    // Apply sensor offsets
    ax -= ax_offset;
    ay -= ay_offset;
    az -= az_offset;
    gx -= gx_offset;
    gy -= gy_offset;
    gz -= gz_offset;

    gx *= PI / 180; // Convert gyro data to radians per second
    gy *= PI / 180;
    gz *= PI / 180;

    float dt = (millis() - previousTime) / 1000.0;
    
    // Calculate the gyroscope component of the orientation quaternion
    float qg0 = cos(alpha * dt / 2.0);
    float qg1 = sin(alpha * dt / 2.0) * gx;
    float qg2 = sin(alpha * dt / 2.0) * gy;
    float qg3 = sin(alpha * dt / 2.0) * gz;
  
    // Calculate the accelerometer component of the orientation quaternion
    float norm = sqrt(ax * ax + ay * ay + az * az);
    if (norm == 0.0)
    {
      return;
    }
    ax /= norm;
    ay /= norm;
    az /= norm;
  
    float qa0 = sqrt((norm + az) / 2.0);
    float qa1 = -ay / (2.0 * qa0);
    float qa2 = ax / (2.0 * qa0);
    float qa3 = 0.0;
  
    // Combine the gyroscope and accelerometer components using the complementary filter
    float q0_temp = qg0 * qa0 - qg1 * qa1 - qg2 * qa2 - qg3 * qa3;
    float q1_temp = qg0 * qa1 + qg1 * qa0 - qg2 * qa3 + qg3 * qa2;
    float q2_temp = qg0 * qa2 + qg1 * qa3 + qg2 * qa0 - qg3 * qa1;
    float q3_temp = qg0 * qa3 - qg1 * qa2 + qg2 * qa1 + qg3 * qa0;
  
    // Normalize the orientation quaternion
    float normq = sqrt(q0_temp * q0_temp + q1_temp * q1_temp + q2_temp * q2_temp + q3_temp * q3_temp);
    if (normq == 0.0)
    {
      return;
    }
    q0 = q0_temp / normq;
    q1 = q1_temp / normq;
    q2 = q2_temp / normq;
    q3 = q3_temp / normq;
  
    previousTime = millis();
  }
}

// Function to get the orientation quaternion
void getOrientation(float* orientation)
{
  orientation[0] = q0;
  orientation[1] = q1;
  orientation[2] = q2;
  orientation[3] = q3;
}

// Example usage of the functions
void setupIMU()
{
  if (!IMU.begin()) {
    Serial.println("Failed to initialize IMU!");
    while (1);
  }
  calibrateSensors();
}

void calibrateSensors() {
  // Collect readings for calibration
  for (int i = 0; i < NUM_READINGS; i++) {
    float ax, ay, az, gx, gy, gz;
    IMU.readAcceleration(ax, ay, az);
    IMU.readGyroscope(gx, gy, gz);
    ax_offset += ax;
    ay_offset += ay;
    az_offset += az;
    gx_offset += gx;
    gy_offset += gy;
    gz_offset += gz;
    delay(CALIBRATION_DELAY_MS / NUM_READINGS);
  }

  // Calculate offsets
  ax_offset /= NUM_READINGS;
  ay_offset /= NUM_READINGS;
  az_offset /= NUM_READINGS;
  gx_offset /= NUM_READINGS;
  gy_offset /= NUM_READINGS;
  gz_offset /= NUM_READINGS;

  Serial.println("Calibration complete.");
}
