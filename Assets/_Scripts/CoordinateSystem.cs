using UnityEngine;

public class CoordinateSystem : MonoBehaviour
{
    public int gridSize = 10;
    public float gridSpacing = 1f;

    void Start()
    {
        // Draw X-axis
        Vector3[] xAxisPoints = new Vector3[] {
            new Vector3(-gridSize / 2f, 0, 0),
            new Vector3(gridSize / 2f, 0, 0)
        };
        GetComponent<LineRenderer>().positionCount = 2;
        GetComponent<LineRenderer>().SetPositions(xAxisPoints);

        // Draw Y-axis
        Vector3[] yAxisPoints = new Vector3[] {
            new Vector3(0, -gridSize / 2f, 0),
            new Vector3(0, gridSize / 2f, 0)
        };
        GameObject yAxis = new GameObject("Y-axis");
        yAxis.transform.parent = transform;
        yAxis.AddComponent<LineRenderer>();
        yAxis.GetComponent<LineRenderer>().widthMultiplier = GetComponent<LineRenderer>().widthMultiplier;
        yAxis.GetComponent<LineRenderer>().positionCount = 2;
        yAxis.GetComponent<LineRenderer>().SetPositions(yAxisPoints);

        // Draw grid lines
        for (int i = 1; i < gridSize; i++)
        {
            Vector3[] verticalGridPoints = new Vector3[] {
                new Vector3(-gridSize / 2f + i * gridSpacing, -gridSize / 2f, 0),
                new Vector3(-gridSize / 2f + i * gridSpacing, gridSize / 2f, 0)
            };
            GameObject verticalGridLine = new GameObject("VerticalGridLine");
            verticalGridLine.transform.parent = transform;
            verticalGridLine.AddComponent<LineRenderer>();
            verticalGridLine.GetComponent<LineRenderer>().widthMultiplier = GetComponent<LineRenderer>().widthMultiplier / 2f;
            verticalGridLine.GetComponent<LineRenderer>().positionCount = 2;
            verticalGridLine.GetComponent<LineRenderer>().SetPositions(verticalGridPoints);

            Vector3[] horizontalGridPoints = new Vector3[] {
                new Vector3(-gridSize / 2f, -gridSize / 2f + i * gridSpacing, 0),
                new Vector3(gridSize / 2f, -gridSize / 2f + i * gridSpacing, 0)
            };
            GameObject horizontalGridLine = new GameObject("HorizontalGridLine");
            horizontalGridLine.transform.parent = transform;
            horizontalGridLine.AddComponent<LineRenderer>();
            horizontalGridLine.GetComponent<LineRenderer>().widthMultiplier = GetComponent<LineRenderer>().widthMultiplier / 2f;
            horizontalGridLine.GetComponent<LineRenderer>().positionCount = 2;
            horizontalGridLine.GetComponent<LineRenderer>().SetPositions(horizontalGridPoints);
        }
    }
}
