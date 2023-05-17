using UnityEngine;

public class GameManager : MonoBehaviour {
   public static GameManager Instance = null; 
   public Question selectedQuestion {get; private set;}
   private const string SERVER_URL = "http://172.20.10.4/";
   private GameObject wonText = null; 
   private GameObject lostText = null; 
   private GameObject startText = null; 
   [SerializeField] public Question[] Questions = new Question[9];
   public GameObject[] symbols = new GameObject[18];
   [SerializeField] private Clock clock = null;
   [SerializeField] public Strikes strikes = null;
   public bool Playing {get; private set;}

   private void Awake() {
        if(Instance == null) {
            Instance = this;
            DontDestroyOnLoad(this);
        } else if(Instance != this) {
            Destroy(this);
        }
        
        wonText = GameObject.Find("WonText");
        lostText = GameObject.Find("LostText");
        startText = GameObject.Find("StartText");
        clock = GameObject.Find("Clock").GetComponent<Clock>();
        strikes = GameObject.Find("Strikes").GetComponent<Strikes>();
    }

    private void Start() => SetGameState(GameState.start);
    
    private void StartScene() {
        Playing = false;
        startText.SetActive(true);
        wonText.SetActive(false);
        lostText.SetActive(false);
    }
    
    private void PlayGame() { 
        Playing = true;
        startText.SetActive(false);
        wonText.SetActive(false);
        lostText.SetActive(false);
        clock.SetClockLenght(300f);  
        strikes.SetStrikesAmount(0); 
        clock.SetClockActive(true);
        ShowAllAnswers(true);
    }

    private void WonGame() {
        Playing = false;
        clock.SetClockActive(false);
        ShowAllAnswers(false);
        wonText.SetActive(true);
    }

    private void LostGame() {
        Playing = false;
        clock.SetClockActive(false);
        ShowAllAnswers(false);
        lostText.SetActive(true);
    }

    public void SetGameState(GameState _gameState) {
        switch (_gameState) {
            case GameState.test:
                clock.SetClockLenght(300f);
                clock.SetClockActive(true);
                strikes.SetStrikesAmount(0);
                break;
            case GameState.start:
                StartScene();
                break;
            case GameState.settings:
                break;
            case GameState.play:
                PlayGame();
                break;
            case GameState.won:
                WonGame();
                break;
            case GameState.lost:
                LostGame();
                break;
            default:
                break;
        }
    }

    private void ShowAllAnswers(bool _state) {
        foreach (Question _q in Questions) {
            _q.ShowAnswers(_state);
        }
    }
    public void SetSelectedQuestion(Question q) => this.selectedQuestion = q;
}

