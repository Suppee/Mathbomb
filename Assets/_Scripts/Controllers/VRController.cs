using System;
using System.Collections.Generic;
using UnityEngine;

public class VRController : MonoBehaviour {
    public static Action<Question> Select;
    [SerializeField] private GameObject targetObject; // The object we want to detect the side of
    private Renderer rendererComponent; // The renderer component of this object
    private Vector3[] localDirections; // An array of local directions for each cube face
    private int lastIndex;
    public void SetRotation(Quaternion q) {
        this.gameObject.transform.rotation = q;
        SideFacingPlayer();
    }

    private void Start() {
        rendererComponent = GetComponent<Renderer>();
        localDirections = new Vector3[] {
            transform.right,
            -transform.right,
            transform.up,
            -transform.up,
            transform.forward,
            -transform.forward
        };
    }

    private void SideFacingPlayer() {
        // Calculate the direction from this object's center to the target object's center
        Vector3 direction = targetObject.transform.position - transform.position;

        // Transform the direction vector into local space
        Vector3 localDirection = transform.InverseTransformDirection(direction);

        // Determine which side of the object is facing the target object
        float maxDot = float.MinValue;
        int maxIndex = -1;
        for (int i = 0; i < localDirections.Length; i++) {
            float dot = Vector3.Dot(localDirection.normalized, localDirections[i].normalized);
            if (dot > maxDot) {
                maxDot = dot;
                maxIndex = i;
            }
        }

        if(lastIndex != maxIndex) {
            // Sets the selected question accordingly by which side of the cube is facing the target object
            switch (maxIndex) {
                case 0:
                    rendererComponent.material.color = Color.red; // Right face
                    Select?.Invoke(GameManager.Instance.Questions[0]);
                    break;
                case 1:
                    rendererComponent.material.color = Color.yellow; // Left face
                    Select?.Invoke(GameManager.Instance.Questions[1]);
                    break;
                case 2:
                    rendererComponent.material.color = Color.green; // Top face
                    Select?.Invoke(GameManager.Instance.Questions[2]);
                    break;
                case 3:
                    rendererComponent.material.color = Color.cyan; // Bottom face
                    Select?.Invoke(GameManager.Instance.Questions[3]);
                    break;
                case 4:
                    rendererComponent.material.color = Color.blue; // Front face
                    Select?.Invoke(GameManager.Instance.Questions[4]);
                    break;
                case 5:
                    rendererComponent.material.color = Color.magenta; // Back face
                    Select?.Invoke(GameManager.Instance.Questions[5]);
                    break;
            }
            FVController.sb.Length = 0; 
            FVController.sb.Append(GameManager.Instance.selectedQuestion.GetAnswered());
        }
        lastIndex = maxIndex;
    }
}
