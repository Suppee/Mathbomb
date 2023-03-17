using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Strikes : MonoBehaviour {
    public TextMesh StrikeText;
    public int StrikesAmount {get; private set;}
    
    public void SetStrikesAmount(int _input) {
        StrikesAmount  = _input;
        if(StrikesAmount == 0) {
            StrikeText.text = "";
        } else if(StrikesAmount == 1) {
            StrikeText.text = "X";
        } else if(StrikesAmount == 2) {
            StrikeText.text = "X X";
        } else {
            StrikeText.text = "X X X";
            GameManager.Instance.SetGameState(GameState.lost);
        }
    }
    private void Awake() {
        StrikeText = this.transform.GetChild(2).GetComponentInChildren<TextMesh>();
    }
}
