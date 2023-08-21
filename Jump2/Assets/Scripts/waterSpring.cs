using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class waterSpring : MonoBehaviour
{
    public float velocity = 0.05f;
    public float force = 0.05f;
    // current height
    public float height = 0.1f;
    // normal height
    private float target_height = 0.1f;
    public Transform springTransform;


    public void WaveSpringUpdate(float springStiffness, float dampening)
    {
        height = transform.localPosition.y;
        // maximum extension
        var x = height - target_height;
        var loss = -dampening * velocity;

        force = -springStiffness * x + loss;
        velocity += force;
        var y = transform.localPosition.y;
        transform.localPosition = new Vector3(transform.localPosition.x, y + velocity, transform.localPosition.z);

    }
   

    
}
