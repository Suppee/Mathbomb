using System.Globalization;
using UnityEngine;
//using KyleDulce.SocketIo;
//using System.IO.Ports;


public class DataReceiver : Data {

    [SerializeField] private VRController vr;
    [SerializeField] private FVController fv;
    //[SerializeField] private bool wifi = true;
    //private bool runLocal = true;
    //private Socket socket;
    //private SerialPort bno055 = new SerialPort("COM7", 115200);
    //private SerialPort keyPad = new SerialPort("COM14", 9600);
    private float w, x, y, z = 0;
    //private string data;

    /*private void SetCurrentBNO055Value(string data)
    {
        currentBNO055Value = data;
        //Debug.Log("CurrentPotValue Received: " + data);
    }

    private void SetCurrentKeyPadValue(string data)
    {
        fv.Character(data);
        //Debug.Log("CurrentPotValue Received: " + data);
    }*/

    private void SetQuat(string data) {       
        //try {
            /*if(wifi) {
                if(currentBNO055Value.Length < 2)
                    return;
                data = currentBNO055Value.Substring(1, currentBNO055Value.Length-2);
            } else {
                data = bno055.ReadLine();
            }*/
            string[] values = data.Split(',');
            
            w = float.Parse(values[3], CultureInfo.InvariantCulture.NumberFormat);
            x = float.Parse(values[1], CultureInfo.InvariantCulture.NumberFormat);
            y = float.Parse(values[0], CultureInfo.InvariantCulture.NumberFormat);
            z = float.Parse(values[2], CultureInfo.InvariantCulture.NumberFormat);

            //xAcc = float.Parse(values[4], CultureInfo.InvariantCulture.NumberFormat);
            //yAcc = float.Parse(values[5], CultureInfo.InvariantCulture.NumberFormat);
            //zAcc = float.Parse(values[6], CultureInfo.InvariantCulture.NumberFormat);
        
            Quaternion q = new Quaternion(-w,-x,y,-z).normalized;
            vr.SetRotation(q);
        //} catch (System.Exception) { 
        //        Debug.Log("timeout"); 
        //}
    }

    private void Start() {
        Setup();
        Events();
        Connection();

        //On "CurrentBNO055Value"
        socket.On("CurrentBNO055Value", (response) => {
            UnityThread.executeInFixedUpdate(() => {
                string input = response.ToString();
                SetQuat(input.Substring(3, input.Length - 6));
            });
        });
                
        //On "CurrentKeyPadValue"
        socket.On("CurrentKeyPadValue", (response) => {
            UnityThread.executeInUpdate(() => {
                string input = response.ToString();
                fv.Character(input.Substring(2, input.Length - 4));
            });
        });

        /*if(wifi) {
            try {
                if (runLocal) {
                    Debug.Log("Connect to Local Server");
                    socket = SocketIo.establishSocketConnection("ws://localhost:3000");
                } else {
                    Debug.Log("Connect to Online Server");
                    socket = SocketIo.establishSocketConnection("ws://sdu-e22-iot-v1.eu-4.evennode.com");
                }

                //Connect to server
                socket.connect();
                Debug.Log("Socket IO - Connected");

                //On "CurrentBNO055Value"
                socket.on("CurrentBNO055Value", SetCurrentBNO055Value);
                
                //On "CurrentKeyPadValue"
                socket.on("CurrentKeyPadValue", SetCurrentKeyPadValue);

            } catch (System.Exception) { 
                Debug.Log("Network issue"); 
            }
        } else {
            try {
                bno055.Open();
                //keyPad.Open();
                bno055.DtrEnable = true;
                //keyPad.DtrEnable = true;
                bno055.ReadTimeout = 1000;
                //keyPad.ReadTimeout = 1000;
            } catch (System.Exception) { 
                Debug.Log("timeout"); 
            }

            if(bno055.IsOpen) {
                Debug.Log("Opened");
            } else {
                Debug.Log("Closed");
            }

            /*if(keyPad.IsOpen) {
                Debug.Log("Opened");
            } else {
                Debug.Log("Closed");
            }*/
        //}
    }
}
