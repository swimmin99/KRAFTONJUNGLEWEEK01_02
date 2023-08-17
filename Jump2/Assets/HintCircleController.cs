using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class HintCircleController : MonoBehaviour
{
    public GameObject exclamationPoint;
    public GameObject exclamationPointMask;

    public float initialMove = 3f;
    public float jumpingDistance = 5f;
    public float finalMove = 5f;
    public float shrinkValue = 0.99f;
    private Rigidbody2D rb;
    public Vector2 JumpForceVector;

    public float JumpForce;
    private bool isJumping = false;

    public float exclamationPointTime = 0.5f;

    public float gaugeUpPos;

    public GameObject HintObject;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    public void StartMovement()
    {
        exclamationPoint.SetActive(true);

        StartInitialMove();
    }

    private void StartInitialMove()
    {
        StartCoroutine(destroyFinal());
        exclamationPoint.SetActive(true);
       

        exclamationPointMask.transform.DOLocalMoveY(gaugeUpPos, exclamationPointTime).OnComplete(() =>
        {
            exclamationPoint.SetActive(false);
        });

        transform.DOMoveX(transform.position.x + initialMove, 1f)
            .SetEase(Ease.Linear)
            .OnComplete(() => StartBounce());


    }

    private void StartBounce()
    {
        exclamationPoint.SetActive(false);
        Vector2 jumpForceVector = JumpForceVector.normalized * JumpForce;

        rb.AddForce(jumpForceVector, ForceMode2D.Impulse);

        isJumping = true;
    }

    private void MoveStraight()
    {
        transform.DOMoveX(transform.position.x + finalMove, 1f)
            .SetEase(Ease.Linear).OnComplete(() => Destroy(gameObject));
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(isJumping == true)
        {
            if (collision.gameObject.CompareTag("Ground"))
                MoveStraight();
        }
    }

    IEnumerator destroyFinal()
    {
        yield return new WaitForSeconds(10f);
        Destroy(gameObject);
    }

}
