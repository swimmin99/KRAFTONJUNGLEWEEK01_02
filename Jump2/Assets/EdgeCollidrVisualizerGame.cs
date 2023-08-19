using UnityEngine;

[RequireComponent(typeof(EdgeCollider2D))]
public class EdgeCollidrVisualizerGame : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public EdgeCollider2D edgeCollider;

    void Start()
    {
        if (lineRenderer == null || edgeCollider == null)
        {
            Debug.LogError("LineRenderer or EdgeCollider2D not assigned!");
            return;
        }

        Vector2[] points2D = edgeCollider.points;
        Vector3[] points3D = new Vector3[points2D.Length];

        for (int i = 0; i < points2D.Length; i++)
        {
            points3D[i] = new Vector3(points2D[i].x, points2D[i].y, 0f);
        }

        lineRenderer.positionCount = points3D.Length;
        lineRenderer.SetPositions(points3D);
    }

}