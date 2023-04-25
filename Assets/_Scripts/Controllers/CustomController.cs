using UnityEngine;

public class CustomController : MonoBehaviour {
    public FVController fv;
    public VRController vr;
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q))
            fv.Character("0"); 
        if(Input.GetKeyDown(KeyCode.W))
            fv.Character(",");
        if(Input.GetKeyDown(KeyCode.E))
            fv.Character("-");
        if(Input.GetKeyDown(KeyCode.R))
            fv.Character("=");
        if(Input.GetKeyDown(KeyCode.T))
            fv.Character("1");
        if(Input.GetKeyDown(KeyCode.Y))
            fv.Character("2");
        if(Input.GetKeyDown(KeyCode.U))
            fv.Character("3");
        if(Input.GetKeyDown(KeyCode.I))
            fv.Character("<");
        if(Input.GetKeyDown(KeyCode.O))
            fv.Character("4");
        if(Input.GetKeyDown(KeyCode.P))
            fv.Character("5");
        if(Input.GetKeyDown(KeyCode.A))
            fv.Character("6");
        if(Input.GetKeyDown(KeyCode.S))
            fv.Character("A");
        if(Input.GetKeyDown(KeyCode.D))
            fv.Character("7");
        if(Input.GetKeyDown(KeyCode.F))
            fv.Character("8");
        if(Input.GetKeyDown(KeyCode.G))
            fv.Character("9");
        if(Input.GetKeyDown(KeyCode.Z))
            vr.SetRotation(Quaternion.Euler(90, 0 ,0));
        if(Input.GetKeyDown(KeyCode.X))
            vr.SetRotation(Quaternion.Euler(270, 0 ,0));
        if(Input.GetKeyDown(KeyCode.V))
            vr.SetRotation(Quaternion.Euler(0, 270 ,0));
        if(Input.GetKeyDown(KeyCode.C))
            vr.SetRotation(Quaternion.Euler(0, 90 ,0));
        if(Input.GetKeyDown(KeyCode.B))
            vr.SetRotation(Quaternion.Euler(0, 0 ,0));
        if(Input.GetKeyDown(KeyCode.N))
            vr.SetRotation(Quaternion.Euler(180, 0 ,0));
    }
}
