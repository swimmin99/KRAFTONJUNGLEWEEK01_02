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

    private void Start()
    {
        myBoxCollider = GetComponent<BoxCollider2D>();
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
        transform.DOScale(new Vector3(0, 0.5f, 0), hideTime/2f).OnComplete(() =>
          {
              myBoxCollider.enabled = false;
              transform.DOScale(Vector3.zero, hideTime/2f).OnComplete(() =>
              {
                  StartCoroutine(WaitHide(hideWaitTime));
              });

          });
        
    }

    IEnumerator WaitHide(float time)
    {
        yield return new WaitForSeconds(time);
        ShowTrap();
    }


    private void ShowTrap()
    {
        myBoxCollider.enabled = true;

        transform.DOScale(Vector3.one, showTime).OnComplete(() =>
        {
            isAnimating = false;
            StartCoroutine(WaitShow(showWaitTime));

        });
    }

    IEnumerator WaitShow(float time)
    {
        yield return new WaitForSeconds(time);
        Invoke("HideTrap", hideTime);
    }

}
