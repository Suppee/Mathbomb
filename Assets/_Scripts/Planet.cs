using UnityEngine;

public class Planet : MonoBehaviour {
    private GameObject player = null;
    private TextMesh text = null;
    [SerializeField] private float distance = 5f;
    [SerializeField] private float speed = 5f;
    [SerializeField] private float radius = 5f;
    private int orbits = 0;
    public bool canOrbit {get; private set;}
    private void Start() {
        this.transform.position = new Vector3(0f, 0f, distance);
        this.transform.localScale *= radius;
        player = GameObject.Find("Player");
        text = this.transform.GetChild(0).GetComponent<TextMesh>();
        text.text = 
            this.gameObject.name + "\n"
            + "Distance: " + this.distance / 10 + " AU" +  "\n" 
            + "Orbit Speed: " + this.speed + " km/s" + "\n"
            + "Radius: " + this.radius;
    }

    private void Update() {
        this.transform.RotateAround(player.transform.position, Vector3.up, speed * Time.deltaTime);
    }

    public void setCanOrbit(bool canOrbit) => this.canOrbit = canOrbit;
}
