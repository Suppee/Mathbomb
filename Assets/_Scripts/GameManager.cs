using System;
using UnityEngine;
using System.IO.Ports;

public class GameManager : MonoBehaviour {
    private static EventHandler ButtonClicked;
    public static GameManager Instance = null;
    private SerialPort stream = new SerialPort("COM7", 9600);
    private GameObject[] planets = new GameObject[8];
    private GameObject planetsParent = null;
    private int alignedPlanets = 0;
    private TextMesh buttonPressed = null;
    private int amount = 0;
    //Creating a Singleton
    private void Awake() {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }    
        else if(Instance != this)
        {
            Destroy(this);
        }
    }
    void Start() {
        planetsParent = GameObject.Find("Planets");
        buttonPressed = this.transform.GetChild(0).GetComponent<TextMesh>();
        buttonPressed.text = amount.ToString();  
        for (int i = 0; i < planets.Length; i++) {
            planets[i] = planetsParent.transform.GetChild(i).gameObject;
        }

        stream.Open();
        stream.ReadTimeout = 25;

        ButtonClicked += OnButtonClicked;
    }
    private void Update() {
        if (stream.IsOpen) {
            try {
                string datastring = stream.ReadLine();
                int dataint = int.Parse(datastring);
                if(dataint == 1) {
                    ButtonClicked?.Invoke(this, null);
                }
            }
            catch (System.Exception) { 
                Debug.Log("timeout"); 
            }
        }
    }
    private void OnButtonClicked(object sender, EventArgs e) {
        amount++;
        buttonPressed.text = amount.ToString();        
    }

}
