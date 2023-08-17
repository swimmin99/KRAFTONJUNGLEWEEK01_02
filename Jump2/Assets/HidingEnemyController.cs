using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class HidingEnemyController : MonoBehaviour
{
    public float initialWaitTime = 2f;
    public float hideTime = 1f;
    public float showTime = 1f;


    public float hideWaitTime = 1f;
    public float showWaitTime = 1f;

    private bool isAnimating = false;
    PolygonCollider2D myPolygonCollider;
    BoxCollider2D myBoxCollider;
    Collider2D myCollider;

    private void Start()
    {
        myPolygonCollider = GetComponent<PolygonCollider2D>();
        myCollider = myPolygonCollider;
        if (myPolygonCollider == null)
        {
            myBoxCollider = GetComponent<BoxCollider2D>();
            myCollider = myBoxCollider;
        }
        Invoke("StartAnimation", initialWaitTime);
    }

    private void StartAnimation()
    {
        if (!isAnimating)
        {
            HideTrap();
        }
    }

    private void HideTrap()
    {
        isAnimating = true;
        transform.DOScale(new Vector3(0,0.1f,0), hideTime).OnComplete(() =>
        {
            myCollider.enabled = false;

            StartCoroutine(Wait1(hideWaitTime));

        });
    }

    IEnumerator Wait1(float time)
    {
        yield return new WaitForSeconds(time);
        ShowTrap();
    }


    private void ShowTrap()
    {
        myCollider.enabled = true;

        transform.DOScale(Vector3.one, showTime).OnComplete(() =>
        {

            isAnimating = false;

            StartCoroutine(Wait2(showWaitTime));

        });
    }

    IEnumerator Wait2(float time)
    {
        yield return new WaitForSeconds(time);
        Invoke("HideTrap", hideTime);
    }

}
