using UnityEngine;

public class Symbol : MonoBehaviour {
    private Vector3 posOffset;
    private Vector3 scaleOffset;
    public GameObject Prefab {get; private set;}
    public bool Spawned = false;
    public int Number {get; private set;}

    public void Spawn(int i) {
        Spawned = true;
        Number = i;
        SetOffsets(Number);
        Prefab = GameManager.Instance.symbols[Number];
        GameObject obj = Instantiate(Prefab, Vector3.zero, Quaternion.identity, this.transform);  
        obj.transform.localPosition = posOffset;
        obj.transform.localScale = scaleOffset;
    }

    private void SetOffsets(int i) {
        switch (i) {
            case 1:
                posOffset = new Vector3(-0.24f, 0, 0);
                scaleOffset = new Vector3(1, 1, 1);
                break;
            case 7:
                posOffset = new Vector3(-0.125f, 0, 0);
                scaleOffset = new Vector3(1, 1, 1);
                break;
            case 10: //*
                posOffset = new Vector3(-0.275f, 0.45f, 0);
                scaleOffset = new Vector3(0.6f, 0.6f, 1);
                break;
            case 12: //+
                posOffset = new Vector3(-0.19f, 0.05f, 0);
                scaleOffset = new Vector3(0.9f, 0.9f, 1);  
                break;
            case 13: //-
            case 14://s
                posOffset = new Vector3(-0.025f, 0.45f, 0);
                scaleOffset = new Vector3(0.8f, 1, 1);  
                break;
            case 16: //%
                posOffset = new Vector3(-0.05f, 0, 0);
                scaleOffset = new Vector3(0.6f, 1, 1);  
                break;
            case 17: //,
                posOffset = new Vector3(-0.22f, 0, 0);
                scaleOffset = new Vector3(1, 1, 1);
                break;
            case 18: //=
                posOffset = new Vector3(-0.03f, 0.35f, 0);
                scaleOffset = new Vector3(0.8f, 0.8f, 1);  
                break;
            default:
                posOffset = new Vector3(0, 0, 0);
                scaleOffset = new Vector3(1, 1, 1);
                break;
        }
    }
}
