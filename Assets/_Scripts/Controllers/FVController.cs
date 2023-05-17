using System.Text;
using UnityEngine;

public class FVController : MonoBehaviour {
    public static StringBuilder sb = new StringBuilder("", 4);

    public void Character(string _response) {
        if(GameManager.Instance.selectedQuestion == null || GameManager.Instance.selectedQuestion.SideCompleted)
            return;
        Question q = GameManager.Instance.selectedQuestion;
        switch (_response) {
            /*case "s":
                if(sb.Length > 0 && sb[0] == '-') sb.Remove(0, 1);
                else sb.Insert(0, '-');
                break;*/
            case "=":
                if(!GameManager.Instance.Playing) {
                    GameManager.Instance.SetGameState(GameState.play);
                    return;
                }
                if(!q.CheckAnswer()) {
                    GameManager.Instance.strikes.SetStrikesAmount(GameManager.Instance.strikes.StrikesAmount + 1);
                    break;
                }
                sb.Length = 0;
                q.NextQuestion();
                return;
            case "<":
                if(sb.Length == 0) break;
                sb.Length--;
                q.RemoveAnswerSymbol();
                break;
            case "A":
                //if(sb.Length == 0) break;
                sb.Length = 0;
                q.RemoveAllSymbols(2);
                break;
            case "N":
                break;
            default:
                if(sb.Length < 6) {
                    sb.Append(_response);
                    q.AddSymbol(_response[0], 2, new Vector3(-1.8f + ((sb.Length - 1) * 0.6f), -1.6f, 0), new Vector3(0.8f, 0.8f, 0.05f));
                }
                break;
        }
        q.SetAnswerText(sb.ToString());
    }
}
