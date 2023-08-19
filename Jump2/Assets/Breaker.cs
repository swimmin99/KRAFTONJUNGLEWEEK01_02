using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breaker : MonoBehaviour
{
    public int collisionCountToBreak = 5;
    public float destructionDelay = 2.0f;

    private int currentCollisionCount = 0;
    private Rigidbody2D rb;
    private bool isBroken = false;
    private float timer = 5f;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!isBroken && collision.gameObject.CompareTag("Player"))
        {
            currentCollisionCount++;
            print("collided");
            if (currentCollisionCount >= collisionCountToBreak)
            {
                BreakObject();
            }
        }



        void BreakObject()
        {
            isBroken = true;

            rb.bodyType = RigidbodyType2D.Dynamic;
            StartCoroutine(destroyer());
        }

        IEnumerator destroyer()
        {
            yield return new WaitForSeconds(timer);
            Destroy(gameObject);
        }
    }

}