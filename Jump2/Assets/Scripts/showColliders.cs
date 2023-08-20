using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class showColliders : MonoBehaviour
{
    [SerializeField] bool showCollider;

    void OnDrawGizmos()
    {
        if (showCollider)
        {
            Gizmos.color = Color.green;
            Gizmos.matrix = this.transform.localToWorldMatrix;
            Gizmos.DrawWireCube(Vector3.zero, Vector3.one);
        }
    }
}
