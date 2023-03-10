using UnityEngine;

public class Question : Selectable {
    public TextMesh[] QuestionText;
    public TextMesh AnswerText;
    private int children;
    public string CorrectAnswer {get => correctAnswer;}
    [SerializeField] private string correctAnswer;
    public bool Correct {get; private set;}
    public override void SetFontStyle(FontStyle style) {
        foreach (TextMesh _tx in QuestionText) {
            _tx.fontStyle = style;
        }
        AnswerText.fontStyle = style;
    }
    public void SetQuestionTextColor(Color _color) {
        AnswerText.color = _color;
    }
    public void SetAnswered(string _input) => this.AnswerText.text = _input;
    public void SetCorrect(bool _correct) => this.Correct = _correct;
 
    private void Awake() {
        QuestionText = this.transform.GetComponentsInChildren<TextMesh>();
    }
}
