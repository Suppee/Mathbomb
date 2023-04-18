using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class QuadraticCurve : MonoBehaviour {
    public float a = 1f;
    public float b = 0f;
    public float c = 0f;
    public float xMin = -5f;
    public float xMax = 5f;
    public int numSegments = 100;

    private LineRenderer lineRenderer;

    private void Start() {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = numSegments;
    }
    void Update() {
        for (int i = 0; i < numSegments; i++) {
            float x = Mathf.Lerp(xMin, xMax, (float)i / (numSegments - 1));
            float y = a * x * x + b * x + c;
            lineRenderer.SetPosition(i, new Vector3(x, y, 0f));
        }
    }
}
