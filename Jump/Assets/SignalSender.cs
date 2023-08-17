using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SignalSender : MonoBehaviour
{
    public HintCircleController destination;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(SignalSending());
            Destroy(gameObject);
        }

    }


    IEnumerator SignalSending()
    {
        destination.StartMovement();
        yield return null;
    }
}
