using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomController : MonoBehaviour
{
    void Update()
    {
       if(Input.GetKeyDown(KeyCode.Q))
            GameManagerV2.Instance.Character("0");
        if(Input.GetKeyDown(KeyCode.W))
            GameManagerV2.Instance.Character(".");
        if(Input.GetKeyDown(KeyCode.E))
            GameManagerV2.Instance.Character("s");
        if(Input.GetKeyDown(KeyCode.R))
            GameManagerV2.Instance.Character("=");
        if(Input.GetKeyDown(KeyCode.T))
            GameManagerV2.Instance.Character("1");
        if(Input.GetKeyDown(KeyCode.Y))
            GameManagerV2.Instance.Character("2");
        if(Input.GetKeyDown(KeyCode.U))
            GameManagerV2.Instance.Character("3");
        if(Input.GetKeyDown(KeyCode.I))
            GameManagerV2.Instance.Character("<");
        if(Input.GetKeyDown(KeyCode.O))
            GameManagerV2.Instance.Character("4");
        if(Input.GetKeyDown(KeyCode.P))
            GameManagerV2.Instance.Character("5");
        if(Input.GetKeyDown(KeyCode.A))
            GameManagerV2.Instance.Character("6");
        if(Input.GetKeyDown(KeyCode.S))
            GameManagerV2.Instance.Character("A");
        if(Input.GetKeyDown(KeyCode.D))
            GameManagerV2.Instance.Character("7");
        if(Input.GetKeyDown(KeyCode.F))
            GameManagerV2.Instance.Character("8");
        if(Input.GetKeyDown(KeyCode.G))
            GameManagerV2.Instance.Character("9");
    }
}
