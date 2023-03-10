using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;

public class Serial : MonoBehaviour
{
    //Stream
    SerialPort stream = new SerialPort("COM9", 9600);

    // Start is called before the first frame update
    void Start()
    {
        stream.Open();
        stream.ReadTimeout = 25;

        if(stream.IsOpen)
        {
            Debug.Log("Opened");
        }
        else
        {
            Debug.Log("Closed");
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if (stream.IsOpen)
        {
            try
            {
                string datastring = stream.ReadLine();
                int dataint = int.Parse(datastring);
                float datafloat = (float)dataint / (float)3.8;

                transform.eulerAngles = new Vector3(0, datafloat, 0);
                Debug.Log(dataint);

            }
            catch (System.Exception) { 
                Debug.Log("timeout"); 
            }
        }
    }
}
