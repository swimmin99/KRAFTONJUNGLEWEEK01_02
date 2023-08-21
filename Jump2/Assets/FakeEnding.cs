using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using Cinemachine;

public class FakeEnding : MonoBehaviour
{
    private Sequence endSequence;
    public GameObject endingText;
    public GameObject player;
    public GameObject parentObj;
    [SerializeField]
    CinemachineVirtualCamera targetCamera;

    private void Start()
    {
        endSequence = DOTween.Sequence().Pause();
        endSequence.Append(endingText.GetComponent<TextMeshProUGUI>().DOFade(1f, .5f)).OnComplete(() => StartCoroutine(exitProgress()));
    }


    IEnumerator exitProgress()
    {
        yield return new WaitForSeconds(3f);
        targetCamera.Follow = player.transform;
        player.GetComponent<Rigidbody2D>().AddForce(new Vector2(1, 1.5f) * 270f);
        player.GetComponent<PlayerController>().enabled = true;

        yield return new WaitForSeconds(2f);

        Destroy(parentObj);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            targetCamera.Follow = endingText.transform;
            endSequence.Play();
            player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            player.GetComponent<PlayerController>().playAnimation();
            player.GetComponent<PlayerController>().enabled = false;
            
            
        }
    }
}
