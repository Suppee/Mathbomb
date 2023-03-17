using UnityEngine;

public class Clock : MonoBehaviour
{
    private TextMesh clockText;   
    public float ClockLenght {get; private set;}
    public bool ClockActive {get; private set;}
    public void SetClockLenght(float _length) => this.ClockLenght = _length;
    public void SetClockActive(bool _state) => this.ClockActive = _state;
    private void DisplayTime(float timeToDisplay) {
        timeToDisplay += 1;
        float minutes = Mathf.FloorToInt(timeToDisplay / 60); 
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        clockText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
    
    private void Awake() {
        clockText = this.transform.GetChild(2).GetComponentInChildren<TextMesh>();
    }
    
    private void Update() {
        if (ClockActive) {
            if (ClockLenght > 0) {
                ClockLenght -= Time.deltaTime;
                DisplayTime(ClockLenght);
            }
            else {
                ClockLenght = 0;
                ClockActive = false;
                GameManager.Instance.SetGameState(GameState.lost);
            }
        }
    }
}
