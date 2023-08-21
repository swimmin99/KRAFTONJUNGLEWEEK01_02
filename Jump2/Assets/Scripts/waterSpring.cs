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
    [SerializeField]
    private static SpriteShapeController spriteShapeController = null;
    private int waveIndex = 0;
    private List<waterSpring> springs = new();
    public void Init(SpriteShapeController ssc)
    {

        var index = transform.GetSiblingIndex();
        waveIndex = index + 1;

        spriteShapeController = ssc;
        velocity = 0;
        height = transform.localPosition.y;
        target_height = transform.localPosition.y;
    }
    // with dampening
    // adding the dampening to the force
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
    public void WavePointUpdate()
    {
        if (spriteShapeController != null)
        {
            Spline waterSpline = spriteShapeController.spline;
            print(waveIndex);
            Vector3 wavePosition = waterSpline.GetPosition(waveIndex);
            waterSpline.SetPosition(waveIndex, new Vector3(wavePosition.x, transform.localPosition.y, wavePosition.z));
        }
    }

    
}
