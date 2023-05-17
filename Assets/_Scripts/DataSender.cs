using UnityEngine;
using UnityEngine.UI;

public class DataSender : Data {
    private static Color lightGreen = new Color(0,1,0,0.75f);
    private static Color lightRed = new Color(1,0,0,0.75f);
    private Button[] buttons;
    [SerializeField] private RawImage connection; 
    private void Awake() {
        buttons = new Button[this.transform.childCount];
        for (int i = 0; i < buttons.Length; i++){
            buttons[i] = this.transform.GetChild(i).GetComponent<Button>();
            int temp = i;
            buttons[i].onClick.AddListener(delegate{SocketEmit(temp);});
        }
    }
    private void Start() {
        ChangeConnectionColor(lightRed);
        Setup();
        Events();
        Connection(); 
    }

    private void SocketEmit(int i) {
        socket.Emit("UpdateKeyPadValue", buttons[i].name);
    }

    protected override void Events() {
        ///// reserved socketio events
        socket.OnConnected += (sender, e) => {
            Debug.Log("socket.OnConnected");
            ChangeConnectionColor(lightGreen);  
        };
        socket.OnPing += (sender, e) => {
            Debug.Log("Ping");
        };
        socket.OnPong += (sender, e) => {
            Debug.Log("Pong: " + e.TotalMilliseconds);
        };
        socket.OnDisconnected += (sender, e) => {
            Debug.Log("Disconnect: " + e);
            ChangeConnectionColor(lightRed);
            
        };
        socket.OnReconnectAttempt += (sender, e) => {
            //Debug.Log($"{DateTime.Now} Reconnecting: attempt = {e}");
        };
    }

    private void ChangeConnectionColor(Color _color) => this.connection.color = _color;
    
}
