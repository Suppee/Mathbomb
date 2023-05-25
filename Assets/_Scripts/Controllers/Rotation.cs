using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation : MonoBehaviour{
    [SerializeField] private Transform mainCamera;
    private Quaternion currentRotation;

    private void Start() {
        mainCamera = Camera.main.transform;
    }

    private void Update() {
        currentRotation = mainCamera.transform.rotation;
        this.gameObject.transform.rotation = Quaternion.Euler(0, currentRotation.eulerAngles.y, 0);
    }
}
