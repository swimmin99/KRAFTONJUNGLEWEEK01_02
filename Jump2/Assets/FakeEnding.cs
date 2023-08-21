using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class FakeEnding : MonoBehaviour
{
    private Sequence endSequence;
    public TextMeshProUGUI endingText;

    private void Start()
    {
        endSequence = DOTween.Sequence().Pause();
        endSequence.Append(endingText.DOFade(1f, .5f)).OnComplete(() => Destroy(gameObject)) ;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            endSequence.Play(); 
        }
    }
}
