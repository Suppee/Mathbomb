using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class ButtonCollector : MonoBehaviour {
    private const string SERVER_URL = "http://172.20.10.4/";

    public IEnumerator CollectButtonValues() {
        while (true) {
            UnityWebRequest request = UnityWebRequest.Get(SERVER_URL);
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success) {
                //string response = request.downloadHandler.text;
                int data = int.Parse(request.downloadHandler.text);
                //GameManagerV2.Instance.Character(data);
            }

            yield return null; // Wait for 1 frame before making another request
        }
    }

    private void Start() {
        StartCoroutine(CollectButtonValues());
    }
}
