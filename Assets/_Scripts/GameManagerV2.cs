using UnityEngine;

public class GameManagerV2 : MonoBehaviour {
   public static GameManagerV2 Instance = null; 
   public Question selectedQuestion;
   private const string SERVER_URL = "http://172.20.10.4/";
   private GameObject questionsParent = null; 
   private GameObject answersParent = null; 
   private GameObject wonText = null; 
   private GameObject lostText = null; 
   private GameObject startText = null; 
   [SerializeField] int questionsAmount = 9;
   [SerializeField] public Question[] Questions = new Question[9];
   [SerializeField] private Clock clock = null;
   [SerializeField] public Strikes strikes = null;
   public int correctAnswered = 0;
   private bool playing = false;

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
            Questions[i] = questionsParent.transform.GetChild(i).GetComponent<Question>();
            Questions[i].AnswerText = answersParent.transform.GetChild(i).GetComponent<TextMesh>();
        }
        foreach (Question que in Questions) {
            que.SetAnswered("");
            que.SetCorrect(false);
        }
        wonText.SetActive(false);
        lostText.SetActive(false);
        SetGameState(GameState.play);
    }

    private void StartGame() {
        questionsParent.SetActive(false);
        answersParent.SetActive(false);
        startText.SetActive(true);
        playing = false;
        correctAnswered = 0;
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

    public void SetGameState(GameState _gameState) {
        switch (_gameState) {
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
                break;
            case GameState.won:
                clock.SetClockActive(false);
                questionsParent.SetActive(false);
                answersParent.SetActive(false);
                wonText.SetActive(true);
                playing = false;
                break;
            default:
                break;
        }
    }
}

