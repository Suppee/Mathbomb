using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class ControllersInput : MonoBehaviour {
    private Quaternion rotation;
    private const string SERVER_URL = "http://192.168.20.212/";
    //private const string SERVER_URL = "http://192.168.1.19/";

    public IEnumerator CollectButtonValues() {
        while (true) {
            UnityWebRequest request = UnityWebRequest.Get(SERVER_URL);
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success) {
                string quaternionString = request.downloadHandler.text;
                string[] quaternionArray = quaternionString.Split(',');
                try {
                    float w = float.Parse(quaternionArray[0]);
                    float x = float.Parse(quaternionArray[1]);
                    float y = float.Parse(quaternionArray[2]);
                    float z = float.Parse(quaternionArray[3]);
                
                    rotation = new Quaternion(w, x, y, z); // convert from Arduino coordinate system to Unity coordinate system
                } catch(System.Exception) {
                    Debug.Log("Fail Reading");
                }

                // Rotate the object using the quaternion
                transform.rotation = rotation;
            }

            yield return null; // Wait for 1 frame before making another request
        }
    }

    private void Start() {
        StartCoroutine(CollectButtonValues());
    }
}
