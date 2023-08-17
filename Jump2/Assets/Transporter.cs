using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transporter : MonoBehaviour
{
    [SerializeField]
    private float movingTime;
    [SerializeField]
    private Transform destinationObject;

    private GameObject player;

 
    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.CompareTag("Player"))
        {
            player = collision.gameObject;
            StartCoroutine(portal());
        }
    }

    IEnumerator portal()
    {
        player.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        yield return new WaitForSeconds(movingTime);
        player.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        player.transform.position = destinationObject.transform.position;
    }
}
