using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;

public class IMURotation : MonoBehaviour
{
    private SerialPort serialPort = new SerialPort("COM3", 9600);
    private Quaternion rotation;

    void Start()
    {
        try
        {
            serialPort.Open();
            serialPort.ReadTimeout = 500;
        }
        catch (System.Exception) 
        { 
            Debug.Log("timeout"); 
        }

        if(serialPort.IsOpen)
        {
            Debug.Log("Opened");
        }
        else
        {
            Debug.Log("Closed");
        }
    }

    void Update()
    {
        if (serialPort.IsOpen)
        {
            try
            {
                Debug.Log("READ"); 
                // Read the quaternion from the serial port
                string quaternionString = serialPort.ReadLine();
                Debug.Log(quaternionString); 
                string[] quaternionArray = quaternionString.Split(',');

                float w = float.Parse(quaternionArray[0]);
                float x = float.Parse(quaternionArray[1]);
                float y = float.Parse(quaternionArray[2]);
                float z = float.Parse(quaternionArray[3]);

                rotation = new Quaternion(-y, -z, x, w); // convert from Arduino coordinate system to Unity coordinate system

                // Rotate the object using the quaternion
                transform.rotation = rotation;
            }
            catch (System.Exception) { 
                Debug.Log("timeout"); 
            }
        }
    }

    void OnApplicationQuit()
    {
        // Close the serial port when the application is quitting
        serialPort.Close();
    }
}
