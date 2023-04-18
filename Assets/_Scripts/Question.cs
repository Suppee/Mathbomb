using System;
using UnityEngine;

public class Question : MonoBehaviour {
    private bool selected;
    public TextMesh[] QuestionText;
    public TextMesh AnswerText;
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
        QuestionText = this.transform.GetComponentsInChildren<TextMesh>();
    }

    private void Selected(Question _selected) {
        selected = _selected == this;
        if(selected && !Correct) {
            style = FontStyle.Bold;
            GameManager.Instance.SetSelectedQuestion(this);
        } else {
            style = FontStyle.Normal;
        }

        foreach (TextMesh _tx in QuestionText) {
            _tx.fontStyle = style;
        }
        AnswerText.fontStyle = style;
    }
    private void OnEnable() => VRController.Select += Selected;
    private void OnDisable() => VRController.Select -= Selected;
}
