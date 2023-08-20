using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breaker : MonoBehaviour
{
    public int collisionCountToBreak = 5;
    public float destructionDelay = 2.0f;
    [SerializeField]
    private int currentCollisionCount = 0;
    private Rigidbody2D rb;
    [SerializeField]
    private bool isBroken = false;
    private float timer = 5f;
    [SerializeField]
    private List<GameObject> animationObject; 

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isBroken && collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            animationObject[currentCollisionCount].SetActive(false);
            currentCollisionCount++;
            animationObject[currentCollisionCount].SetActive(true);
            if (currentCollisionCount >= collisionCountToBreak)
            {
                BreakObject();
            }
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

