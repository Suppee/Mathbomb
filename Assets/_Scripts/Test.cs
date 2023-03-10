using System.Text;
using UnityEngine;

public class Test : MonoBehaviour
{
    // Start is called before the first frame update
    private StringBuilder sb = new StringBuilder("1", 2);
    [SerializeField] private string s;
    void Start()
    {
        sb.Append("1");
        if(s == sb.ToString())
            Debug.Log("Alike");

        if(s.Equals(sb.ToString())) 
            Debug.Log("Alike");
    }
}
