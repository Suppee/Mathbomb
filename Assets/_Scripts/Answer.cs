using UnityEngine;

public class Answer : MonoBehaviour {
    public TextMesh AnswerText {get; private set;}
    [SerializeField] private string correctAnswer;
    public bool Correct {get; private set;}
    
    
    public void setAnswerFontStyle(FontStyle style) => AnswerText.fontStyle = style;
    public void setAnswered(string input) {
        AnswerText.text = input;
    }
    private void setCorrect(bool correct) => this.Correct = correct;

    private void Awake() {
        AnswerText = this.transform.GetComponent<TextMesh>();
    }

}
