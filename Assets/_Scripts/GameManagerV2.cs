using System.Text;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class GameManagerV2 : MonoBehaviour {
   public static GameManagerV2 Instance = null; 
   private const string SERVER_URL = "http://172.20.10.4/";
   private GameObject questionsParent = null; 
   private GameObject answersParent = null; 
   private GameObject wonText = null; 
   private GameObject lostText = null; 
   private GameObject startText = null; 
   [SerializeField] int questionsAmount = 9;
   [SerializeField] private Question[] questions = new Question[9];
   [SerializeField] private Clock clock = null;
   [SerializeField] private Strikes strikes = null;
   private StringBuilder sb = new StringBuilder("", 4);
   private bool selector = true;
   private Question selectedQuestion;
   [SerializeField] private Color normal;
   [SerializeField] private Color correct;
   [SerializeField] private Color wrong;
   private int correctAnswered = 0;
   private bool playing = false;
   private bool signed = false;

   private void Awake() {
        if(Instance == null) {
            Instance = this;
            DontDestroyOnLoad(this);
        }    
        else if(Instance != this) {
            Destroy(this);
        }

        questionsParent = GameObject.Find("Questions");
        answersParent = GameObject.Find("Answers");
        wonText = GameObject.Find("Won");
        lostText = GameObject.Find("Lost");
        startText = GameObject.Find("Start");
        
        clock = GameObject.Find("Clock").GetComponent<Clock>();
        strikes = GameObject.Find("Strikes").GetComponent<Strikes>();
    }

    private void Start() {
        for (int i = 0; i < questionsAmount; i++) {
            questions[i] = questionsParent.transform.GetChild(i).GetComponent<Question>();
            questions[i].AnswerText = answersParent.transform.GetChild(i).GetComponent<TextMesh>();
        }
        StartCoroutine(CollectButtonValues());
        SetGameState(GameState.start);
    }

    private IEnumerator CollectButtonValues() {
        while (true) {
            UnityWebRequest request = UnityWebRequest.Get(SERVER_URL);
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success) {
                string response = request.downloadHandler.text;  
                Character(response);
            }

            yield return null; // Wait for 1 frame before making another request
        }
    }

    private void StartGame() {
        questionsParent.SetActive(false);
        answersParent.SetActive(false);
        wonText.SetActive(false);
        lostText.SetActive(false);
        startText.SetActive(true);
        playing = false;
        selector = true;
        sb.Length = 0;
        correctAnswered = 0;

        foreach (Question que in questions) {
            que.SetAnswered("?");
            que.SetCorrect(false);
            que.SetQuestionTextColor(normal);
        }
    }
    
    private void PlayGame() {
        playing = true;
        startText.SetActive(false);
        questionsParent.SetActive(true);
        answersParent.SetActive(true);
        clock.SetClockLenght(300f);
        clock.SetClockActive(true);
        strikes.SetStrikesAmount(0);
    }

    private void QuestionSelected(Question _question) {
        if(playing == false) {
            return;
        }
        if(_question.Correct) {
            return;
        }

        selectedQuestion = _question;
        _question.SetFontStyle(FontStyle.Bold);
        selector = false;
    }

    private void Deselect() {
        foreach (Question ques in questions) {
            ques.SetFontStyle(FontStyle.Normal);
        }
        selectedQuestion = null;
    }

    public void SetGameState(GameState _gameState) {
        switch (_gameState)
        {
            case GameState.test:
                clock.SetClockLenght(300f);
                clock.SetClockActive(true);
                strikes.SetStrikesAmount(0);
                break;
            case GameState.start:
                StartGame();
                break;
            case GameState.settings:
                break;
            case GameState.play:
                PlayGame();
                break;
            case GameState.lost:
                clock.SetClockActive(false);
                questionsParent.SetActive(false);
                answersParent.SetActive(false);
                lostText.SetActive(true);
                playing = false;
                selector = true;
                break;
            case GameState.won:
                clock.SetClockActive(false);
                questionsParent.SetActive(false);
                answersParent.SetActive(false);
                wonText.SetActive(true);
                playing = false;
                selector = true;
                break;
            default:
                break;
        }
    }

    public void Character(string _response) {
        if(selector) {
            switch (_response)
            {
                case "0": //0
                    break;
                case ".": //.
                    break;
                case "s": //+/-
                    break;
                case "=": // =
                    if(playing == false) {
                        SetGameState(GameState.play);
                    }
                    break;
                case "1": //1
                    QuestionSelected(questions[0]);
                    break;
                case "2": //2
                    QuestionSelected(questions[1]); 
                    break;
                case "3": //3
                    QuestionSelected(questions[2]);
                    break;
                case "<": //Delete last character 
                    if(playing == false) {
                        SetGameState(GameState.start);
                    }
                    break;
                case "4": //4
                    QuestionSelected(questions[3]);
                    break;
                case "5": //5
                    QuestionSelected(questions[4]);
                    break;
                case "6": //6
                    QuestionSelected(questions[5]);
                    break;
                case "A": //Delete all characters
                    Deselect();
                    break;
                case "7": //7
                    QuestionSelected(questions[6]);
                    break;
                case "8": //8
                    QuestionSelected(questions[7]);
                    break;
                case "9": //9
                    QuestionSelected(questions[8]);
                    break;
                default:
                    break;
            }
        } else {
            switch (_response)
            {
                case "0": //0
                    if(sb.Length > 4) {
                        break;
                    }
                    selectedQuestion.SetQuestionTextColor(normal);
                    sb.Append("0");
                    break;
                case ".": //.
                    if(sb.Length > 4) {
                        break;
                    }
                    selectedQuestion.SetQuestionTextColor(normal);
                    sb.Append(".");
                    break;
                case "s": //+/-
                    selectedQuestion.SetQuestionTextColor(normal);
                    if(signed) {
                        sb.Remove(0, 1);
                        signed = false;
                    } else {
                        sb.Insert(0,"-");
                        signed = true;
                    }
                    break;
                case "=": // =
                    if(selectedQuestion.CorrectAnswer == sb.ToString()) { //Does not work
                        selectedQuestion.SetCorrect(true);
                        selectedQuestion.SetQuestionTextColor(correct);
                        correctAnswered++;
                        selector = true;
                        sb.Length = 0;
                        Deselect();
                        if(correctAnswered == 9) {
                            SetGameState(GameState.won);
                            return;
                        }
                        return;
                    }
                    if(sb.Length > 0 ) {
                        selectedQuestion.SetQuestionTextColor(wrong);
                        strikes.SetStrikesAmount(strikes.StrikesAmount + 1);
                    }
                    break;
                case "1": //1
                    if(sb.Length > 4) {
                        break;
                    }
                    selectedQuestion.SetQuestionTextColor(normal);
                    sb.Append("1");
                    break;
                case "2": //2
                    if(sb.Length > 4) {
                        break;
                    }
                    selectedQuestion.SetQuestionTextColor(normal);
                    sb.Append("2");
                    break;
                case "3": //3
                    if(sb.Length > 4) {
                        break;
                    }
                    selectedQuestion.SetQuestionTextColor(normal);
                    sb.Append("3");
                    break;
                case "<": //Delete last character 
                    selectedQuestion.SetQuestionTextColor(normal);
                    if(sb.Length == 0) {
                        break;
                    } 
                    
                    sb.Length--;
                    if(sb.Length == 0) {
                        signed = false;
                    } 
                    break;
                case "4": //4
                    if(sb.Length > 4) {
                        break;
                    }
                    selectedQuestion.SetQuestionTextColor(normal);
                    sb.Append("4");
                    break;
                case "5": //5
                    if(sb.Length > 4) {
                        break;
                    }
                    selectedQuestion.SetQuestionTextColor(normal);
                    sb.Append("5");
                    break;
                case "6": //6
                    if(sb.Length > 4) {
                        break;
                    }
                    selectedQuestion.SetQuestionTextColor(normal);
                    sb.Append("6");
                    break;
                case "A": //Delete all characters
                    selectedQuestion.SetQuestionTextColor(normal);
                    if(sb.Length == 0) {
                        selector = true;
                        Deselect();
                        return;
                    }
                    sb.Length = 0;
                    signed = false;
                    break;
                case "7": //7
                    if(sb.Length > 4) {
                        break;
                    }
                    selectedQuestion.SetQuestionTextColor(normal);
                    sb.Append("7");
                    break;
                case "8": //8
                    if(sb.Length > 4) {
                        break;
                    }
                    selectedQuestion.SetQuestionTextColor(normal);
                    sb.Append("8");
                    break;
                case "9": //9
                    if(sb.Length > 4) {
                        break;
                    }
                    selectedQuestion.SetQuestionTextColor(normal);
                    sb.Append("9");
                    break;
                default:
                    break;
            }
            selectedQuestion.SetAnswered(sb.ToString());
        }  
    }
}
