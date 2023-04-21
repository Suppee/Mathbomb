using UnityEngine;
using TMPro;

public class Question : MonoBehaviour {
    private bool selected;
    public TMP_Text[] QuestionText;
    public TMP_Text AnswerText;
    private FontStyle style;
    public string CorrectAnswer {get => correctAnswer;}
    [SerializeField] private string correctAnswer;
    public bool Correct {get; private set;}
    
    public void SetQuestionTextColor(Color _color) {
        AnswerText.color = _color;
    }

    public void SetAnswered(string _input) => this.AnswerText.text = _input;
    public string GetAnswered() => this.AnswerText.text;
    public void SetCorrect(bool _correct) => this.Correct = _correct;
 
    private void Awake() {
        QuestionText = this.transform.GetComponentsInChildren<TMP_Text>();
    }

    private void Selected(Question _selected) {
        selected = _selected == this;
        if(selected && !Correct) {
            style = FontStyle.Bold;
            GameManager.Instance.SetSelectedQuestion(this);
        } else {
            style = FontStyle.Normal;
        }

        foreach (TMP_Text _tx in QuestionText) {
            _tx.fontStyle = (FontStyles)style;
        }
        AnswerText.fontStyle = (FontStyles)style;
    }
    private void OnEnable() => VRController.Select += Selected;
    private void OnDisable() => VRController.Select -= Selected;
}
