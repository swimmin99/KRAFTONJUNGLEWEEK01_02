using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using Cinemachine;

public class PlayerEndingController : MonoBehaviour
{
    private Sequence endSequence;
    public TextMeshProUGUI endingText;
    public TextMeshProUGUI creditText;
    public Transform endingPos;
    public GameManager GameManager;
    public PlayerController playerController;
    public GameObject cameraLoc;
    public GameObject playerObj;
    public Transform leg1;
    public Transform leg2;

    private bool isWalking = false;
    private bool isEnd=false;

    [SerializeField]
    CinemachineVirtualCamera targetCamera;
    //  targetCamera.Follow = GameObject.FindGameObjectWithTag("Player").transform;
    private void Start()
    {
        endSequence = DOTween.Sequence().Pause()
           .Append(playerObj.transform.DOMove(endingPos.position, 10f).OnComplete(() =>
           {
               targetCamera.Follow = cameraLoc.transform;
           }) )
           .Append(endingText.DOFade(1f, 3f))
           .Append(endingText.DOFade(1f, 3f))
           .Append(endingText.DOFade(0f, 3f))
           .Append(creditText.DOFade(1f, 3f))
           .Append(creditText.DOFade(0f, 3f))
           .OnComplete(() => GameManager.ActiveEndUI());

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            EndingPlay();
            playerController.enabled = false;
        }
    }



    private void MoveLeg(Transform leg1, Transform leg2, float MoveAngleAbs)
    {
        isWalking = true;
        leg1.DOLocalRotate(new Vector3(0f, 0f, MoveAngleAbs), 0.1f).OnComplete(() => leg1.DOLocalRotate(new Vector3(0f, 0f, -MoveAngleAbs),0.1f).OnComplete(() => isWalking = false));
        leg2.DOLocalRotate(new Vector3(0f, 0f, -MoveAngleAbs), 0.1f).OnComplete(() => leg2.DOLocalRotate(new Vector3(0f, 0f, MoveAngleAbs), 0.1f).OnComplete(() => isWalking = false));
    }

    public void EndingPlay()
    {
        isEnd = true;
        
        endSequence.Play();
        
    }

    private void Update()
    {
        if (isEnd)
        {
            if (!isWalking)
            {
                MoveLeg(leg1, leg2, 10f);
            }
        }
        
    }

}
