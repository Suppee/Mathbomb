using System.Text;
using UnityEngine;

public class Test : MonoBehaviour {
    [SerializeField] private string[] s;
    [SerializeField] private Question[] q;
  
    private void Update() {
        if(Input.GetKeyDown(KeyCode.Space)) {
            for (int i = 0; i < q.Length; i++) {
                q[i].SpawnQuestion(s[i]);
            }   
        }
    }
}
