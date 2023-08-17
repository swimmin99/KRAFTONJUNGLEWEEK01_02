using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceBlock : MonoBehaviour
{
    [SerializeField] public bool toTarget;
    [SerializeField] public Transform target;
    [SerializeField] [Range(0f, 360f)] public float deg;
    [SerializeField] public float force;
    private float directionX;
    private float directionY;
    private Vector2 direction;
    private Vector2 bounceVector;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            BouncingPlayer(collision.rigidbody);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            BouncingPlayer(collision.gameObject.GetComponent<Rigidbody2D>());
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            BouncingPlayer(collision.gameObject.GetComponent<Rigidbody2D>());
        }
    }

    float ConvertDegToRad(float deg)
    {
        return (deg * (Mathf.PI / 180));
    }

    void BouncingPlayer(Rigidbody2D rg)
    {
        rg.velocity = Vector2.zero;
        if (toTarget)
        {
            directionX = target.position.x - transform.position.x;
            directionY = target.position.y - transform.position.y;
        }
        else
        {
            directionX = Mathf.Cos(ConvertDegToRad(deg));
            directionY = Mathf.Sin(ConvertDegToRad(deg));
        }
        direction = new Vector2(directionX, directionY);
        bounceVector = direction.normalized * force;
        rg.AddForce(bounceVector, ForceMode2D.Impulse);
    }

}
