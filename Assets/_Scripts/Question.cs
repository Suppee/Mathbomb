using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Question : MonoBehaviour {
    private bool selected;
    public bool Unlocked {get; private set;} 
    public int CurrentQuestion {get; private set;}
    [SerializeField] private string[] header;
    [SerializeField] private string[] questions; //First questions unlocks side
    [SerializeField] private string[] answers;
    [SerializeField] private TextMesh answer;
    public string AnswerText {get; private set;}
    private List<Symbol> headerSymbols = new List<Symbol>();
    private List<Symbol> questionSymbols = new List<Symbol>();
    private List<Symbol> answerSymbols = new List<Symbol>();
    public bool SideCompleted {get; private set;}
    public void SetUnlocked(bool _unlocked) => this.Unlocked = _unlocked;
    public void SetCurrentQuestion(int _current) => this.CurrentQuestion = _current;
    public void SetAnswerText(string _text) => this.AnswerText = _text;
    public void SetSideCompleted(bool _complete) => this.SideCompleted = _complete;

    private void SpawnHeader(string _header) {
        char[] symbols = _header.ToCharArray();
        float position = 0;
        for (int i = 0; i < symbols.Length; i++) {
            if(position == 2 && CurrentQuestion != 0) {
                position += 0.5f;
            }
            AddSymbol(symbols[i], 0, new Vector3(-1.75f + (position * 0.3f), 1.45f, 0), new Vector3(0.4f, 0.4f, 0.05f));  
            position++;
        }
    }

    public void SpawnQuestion(string _question) {
        char[] symbols = _question.ToCharArray(); // ^ == exponentional, \ == squared, f == fraction
        float position = 0;
        for (int i = 0; i < symbols.Length; i++) {
            if(symbols[i] == '^') {
                AddSymbol(symbols[i + 1], 1, new Vector3(-1.8f + ((position - 1) * 0.6f + 0.4f), 0.4f, 0), new Vector3(0.4f, 0.4f, 0.05f));
                i++; 
            } else if(symbols[i] == '\\') { 
                AddSymbol('\\', 1, new Vector3(-1.8f + (position * 0.6f), -0.4f, 0), new Vector3(0.8f, 0.8f, 0.05f));
                position += 2;
                i++;
                while(symbols[i] != '\\') { //then everything is squard until next \.
                    if(symbols[i] == '^') {
                        AddSymbol(symbols[i + 1], 1, new Vector3(-1.8f + (position * 0.4f - 0.13f), 0.1f, 0), new Vector3(0.2f, 0.2f, 0.05f));
                        i += 2; 
                    } else {        
                        AddSymbol(symbols[i], 1, new Vector3(-1.8f + (position * 0.4f), -0.4f, 0), new Vector3(0.5f, 0.5f, 0.05f));
                        AddSymbol('-', 1, new Vector3(-1.8f + (position * 0.4f), -0.06f, 0), new Vector3(1, 0.8f, 0.05f));
                        position++;
                        i++;
                    }

                    if(i >= symbols.Length) {
                        break;
                    } 
                }
                AddSymbol('.', 1, new Vector3(-1.8f + (position * 0.4f + 0.1f) , 0.203f, 0), new Vector3(0.4f, 1, 0.05f));
                position = position > 3? position - 1 : position - 0.5f;
            } else if(symbols[i] == 'f') {
                //while f then everything is stil in the fraction. - == divider - determines over or under dividerline 
                //needs to center if either two or more numbers over or under dividerline
                //then everything is fraction until next  f.
                bool overline = true;
                i++;
                AddSymbol('-', 1, new Vector3(-1.8f + (position * 0.6f), -0.49f, 0), new Vector3(0.8f, 0.8f, 0.05f));
                while(symbols[i] != 'f') {
                    if(symbols[i] == '-') {
                        overline = false;
                        i++;
                    }
                    else if(overline) {
                        AddSymbol(symbols[i], 1, new Vector3(-1.8f + (position * 0.6f + 0.07f), 0.1f, 0), new Vector3(0.6f, 0.6f, 0.05f));
                        i++;
                    } else {
                        AddSymbol(symbols[i], 1, new Vector3(-1.8f + (position * 0.6f + 0.07f), -0.8f, 0), new Vector3(0.6f, 0.6f, 0.05f));
                        i++;
                    }
                }
                position++;
            } else {
                AddSymbol(symbols[i], 1, new Vector3(-1.8f + (position * 0.6f), -0.4f, 0), new Vector3(0.8f, 0.8f, 0.05f));
                position++;
            }     
        }
    }

    public void SpawnAmountSymbols() {
        for (int i = 0; i < questions.Length - 1; i++) {
            AddSymbol('.', 3, new Vector3(1.55f, 1.1f - (i * 0.3f), 0), new Vector3(1, 1, 0.05f));  
        }
    }

    public void AddSymbol(char _symbol, int _list, Vector3 _pos, Vector3 _scale) {
        Symbol symbol = PoolHandler.Instance.Get(Helper.Prefab2Spawn(_symbol));
        switch (_list)
        {
            case 0:
                headerSymbols.Add(symbol);
                break;
            case 1: 
                questionSymbols.Add(symbol);
                break;
            case 2: 
                answerSymbols.Add(symbol);
                break;
            default:
                break;
        }
        symbol.transform.parent = this.transform;
        symbol.transform.localPosition = _pos;
        symbol.transform.localRotation = Quaternion.Euler(0, 180f, 0);
        symbol.transform.localScale = _scale;
    }

    public void RemoveAnswerSymbol() {
        int number = answerSymbols.Count - 1;
        PoolHandler.Instance.Release(answerSymbols[number], answerSymbols[number].Number);
        answerSymbols.Remove(answerSymbols[number]);
    }

    public void RemoveAllSymbols(int _list) {
        List<Symbol> list = null;
        switch (_list)
        {
            case 0:
                list = headerSymbols;
                break;
            case 1: 
                list = questionSymbols;
                break;
            case 2: 
                list = answerSymbols;
                break;
            default:
                break;
        }
        for (int i = 0; i < list.Count; i++) {
            PoolHandler.Instance.Release(list[i], list[i].Number);
        }
        list.Clear();
    }

    public void NextQuestion() {
        RemoveAllSymbols(1);
        RemoveAllSymbols(2);
        SetCurrentQuestion(CurrentQuestion + 1); 
        if(CurrentQuestion == 1) {
            RemoveAllSymbols(0);
            SpawnHeader(header[1]);
            answer.text = header[1] + '\n';
            FormatText();
        }
        else if(CurrentQuestion == questions.Length) {
            SetSideCompleted(true);
            Debug.Log("Completed Side");
            return;
        }

        if(CurrentQuestion != 1) {
            FormatText();
        }
        SpawnQuestion(questions[CurrentQuestion]);
    }

    public bool CheckAnswer() {
        if(answers[CurrentQuestion] == AnswerText) {
            return true;
        } else {
            return false;
        }
    }

    private void FormatText() {
        int lastQuestion = CurrentQuestion - 1; 
        answer.text += lastQuestion + 1 + ". " + answers[lastQuestion] + '\n';
                        
    }

    private void Selected(Question _selected) {
        selected = _selected == this;
        if(selected) GameManager.Instance.SetSelectedQuestion(this);
    }

    private void Start() {
        SetSideCompleted(false);
        SpawnHeader(header[0]);
        SpawnQuestion(questions[0]);
        SpawnAmountSymbols();
        SetCurrentQuestion(0);
    }

    private void OnEnable() => VRController.Select += Selected;
    private void OnDisable() => VRController.Select -= Selected;
}
