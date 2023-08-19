using UnityEngine;

[RequireComponent(typeof(EdgeCollider2D))]
public class ShowCollider : MonoBehaviour
{
    private EdgeCollider2D edgeCollider;

    private void Start()
    {
        edgeCollider = GetComponent<EdgeCollider2D>();
    }

    private void OnDrawGizmos()
    {
        if (edgeCollider == null)
            return;

        Vector2[] points = edgeCollider.points;

        Gizmos.color = Color.green;

        for (int i = 0; i < points.Length - 1; i++)
        {
            Vector3 start = transform.TransformPoint(points[i]);
            Vector3 end = transform.TransformPoint(points[i + 1]);

            Gizmos.DrawLine(start, end);
        }
    }
}