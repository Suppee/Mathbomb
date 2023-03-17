using System.Text;
using UnityEngine;

public class FVController : MonoBehaviour {
    public static StringBuilder sb = new StringBuilder("", 4);
    [SerializeField] private Color normal;
    [SerializeField] private Color correct;
    [SerializeField] private Color wrong;
    public void Character(string _response) {
        if(GameManager.Instance.selectedQuestion == null || GameManager.Instance.selectedQuestion.Correct)
            return;
        GameManager.Instance.selectedQuestion.SetQuestionTextColor(normal);
        char characterToAdd = ' '; 
        switch (_response) {
            case "0": //0
                characterToAdd = '0';
                break;
            case ".": //.
                characterToAdd = '.';
                break;
            case "s": //+/-
                if(sb.Length > 0 && sb[0] == '-') {
                    sb.Remove(0, 1);
                } else {
                    sb.Insert(0,"-");
                }
                break;
            case "=": // =
                if(GameManager.Instance.selectedQuestion.CorrectAnswer == sb.ToString()) {
                    GameManager.Instance.selectedQuestion.SetCorrect(true);
                    GameManager.Instance.selectedQuestion.SetQuestionTextColor(correct);
                    GameManager.Instance.correctAnswered++;
                    if(GameManager.Instance.correctAnswered == 9) {
                        GameManager.Instance.SetGameState(GameState.won);
                        return;
                    }
                    return;
                }
                if(sb.Length > 0 ) {
                    GameManager.Instance.selectedQuestion.SetQuestionTextColor(wrong);
                    GameManager.Instance.strikes.SetStrikesAmount(1 + GameManager.Instance.strikes.StrikesAmount);
                }
                break;
            case "1": //1
                characterToAdd = '1';
                break;
            case "2": //2
                characterToAdd = '2';
                break;
            case "3": //3
                characterToAdd = '3';
                break;
            case "<": //Delete last character 
                if(sb.Length == 0) {
                    break;
                } 
            
                sb.Length--;
                break;
            case "4": //4
                characterToAdd = '4';
                break;
            case "5": //5
                characterToAdd = '5';
                break;
            case "6": //6
                characterToAdd = '6';
                break;
            case "A": //Delete all characters
                if(sb.Length == 0) {
                    break;
                }
                sb.Length = 0;
                break;
            case "7": //7
                characterToAdd = '7';
                break;
            case "8": //8
                characterToAdd = '8';
                break;
            case "9": //9
                characterToAdd = '9';
                break;
            default:
                break;
        }
        if(sb.Length < 4 && characterToAdd != ' ') 
            sb.Append(characterToAdd);

        GameManager.Instance.selectedQuestion.SetAnswered(sb.ToString());
    }
        
}
