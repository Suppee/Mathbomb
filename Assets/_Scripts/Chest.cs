using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour {
    [SerializeField] private Animator anim;
    [SerializeField] private bool open = false;
    private void Start() {
        anim = GetComponent<Animator>();
    }

    private void Update() {
        if(open) {
            anim.CrossFade("Open", 0, 0);
        } else {
            anim.CrossFade("Close", 0, 0);
        }
    }
}
