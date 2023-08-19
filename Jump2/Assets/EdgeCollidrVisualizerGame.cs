using UnityEngine;

[RequireComponent(typeof(EdgeCollider2D))]
public class EdgeCollidrVisualizerGame : MonoBehaviour
{
    private EdgeCollider2D edgeCollider;
    private LineRenderer lineRenderer;

    private void Start()
    {
        edgeCollider = GetComponent<EdgeCollider2D>();

        lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        lineRenderer.startColor = Color.green;
        lineRenderer.endColor = Color.green;
        lineRenderer.startWidth = 0.05f;
        lineRenderer.endWidth = 0.05f;

        UpdateLineRenderer();
    }

    private void Update()
    {
        UpdateLineRenderer();
    }

    private void UpdateLineRenderer()
    {
        if (edgeCollider == null || lineRenderer == null)
            return;

        Vector2[] points = edgeCollider.points;

        lineRenderer.positionCount = points.Length;

        for (int i = 0; i < points.Length; i++)
        {
            lineRenderer.SetPosition(i, points[i]);
        }
    }
}
