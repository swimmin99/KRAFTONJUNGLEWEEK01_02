using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerController : MonoBehaviour
{

    public GameObject player;
    public Rigidbody2D playerRigid;
    public float playerFallGravityScale;
    public float playerMaxVelocity;
    public float playerMoveSpeed;
    public float playerMaxSpeed;
    public Vector2 jumpDirection;
    public bool isPlayerJump;
    public bool isPlayerMove;
    private bool isGrounded;
    public float jumpForce;
    public float jumpLimitTime;
    public int maxJumpStep;
    public float jumpTimer;

    public float minBackBounceX;
    public float maxBackBounceX;

    public float minBackBounceY;
    public float maxBackBounceY;

    public float maxBackBounceForce;
    public float minBackBounceForce;

    public ForceGameController forceGameController;

    public float minJumpValue;

    public Transform leg1;
    public Transform leg2;
    public float stepDuration = 0.1f;
    private bool isWalking = false;

    [SerializeField]
    private float BounceAnmiationTime = 0.2f;
    public GameObject BounceEye;
    private bool BounceAnimOn = false;

    void Start()
    {
        player = gameObject;
        playerRigid = gameObject.GetComponent<Rigidbody2D>();
    }

    void PlayerMove()
    {
        if (isPlayerMove)
        {
            if (Input.GetKey(KeyCode.RightArrow))
            {
                if (playerRigid.velocity.magnitude < playerMaxSpeed)
                {
                    playerRigid.AddForce(Vector2.right * playerMoveSpeed);
                }
                
            }

            else if (Input.GetKey(KeyCode.LeftArrow))
            {
                if (playerRigid.velocity.magnitude < playerMaxSpeed)
                {
                    playerRigid.AddForce(-Vector2.right * playerMoveSpeed);
                }
            }

            if(isWalking == false&&Mathf.Abs(playerRigid.velocity.x)>0.2f)
            {
                MoveLeg(leg1, leg2, 10f);
            }

        }
        
    }

    private void MoveLeg(Transform leg1, Transform leg2, float MoveAngleAbs)
    {
        isWalking = true;
        leg1.DOLocalRotate(new Vector3(0f, 0f, MoveAngleAbs), stepDuration).OnComplete(() => leg1.DOLocalRotate(new Vector3(0f, 0f, -MoveAngleAbs), stepDuration).OnComplete(() => isWalking = false));
        leg2.DOLocalRotate(new Vector3(0f, 0f, -MoveAngleAbs), stepDuration).OnComplete(() => leg2.DOLocalRotate(new Vector3(0f, 0f, MoveAngleAbs), stepDuration).OnComplete(() => isWalking = false));
    }



    void PlayerJump()
    {
        if (isPlayerJump)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                forceGameController.gameObject.SetActive(true);
                forceGameController.IncreaseSpeedGauge(jumpLimitTime * 1.5f);
                isGrounded = true;
            }

            if (Input.GetKey(KeyCode.Space))
            {
                if(isGrounded)
                {
                    if (jumpTimer < jumpLimitTime)
                    {
                        jumpTimer += Time.deltaTime;
                        Debug.Log(jumpTimer);
                    }
                }
            }

            if (Input.GetKeyUp(KeyCode.Space))
            {
                isGrounded = false;
                float interval = jumpLimitTime / (float)maxJumpStep;
                float jumpStep = 0f;
                for (int i = 0; i < maxJumpStep; i++)
                {
                    if (jumpTimer <= 0.05f)
                    {
                        jumpStep = 0.05f;
                        break;
                    }
                    if (jumpStep - 0.05f <= jumpTimer && jumpStep + 0.05f >= jumpTimer)
                    {
                        break;
                    }
                    jumpStep += interval;
                }
                Debug.Log("jumpStep" + jumpStep);

                Vector2 jumpForceVector = jumpDirection.normalized * jumpStep * jumpForce;
                playerRigid.AddForce(jumpForceVector, ForceMode2D.Impulse);
                jumpTimer = 0;

            }
        }

    }

    void PlayerBounceBack()
    {
        float randomDirectionX = Random.Range(minBackBounceX, maxBackBounceX);
        float randomDirectionY = Random.Range(minBackBounceY, maxBackBounceY);
        Vector2 randomDirection = new Vector2(randomDirectionX, randomDirectionY);
        float randomJumpForce = Random.Range(minBackBounceForce, maxBackBounceForce);
        Debug.Log("nowForce : " + randomJumpForce);
        Vector2 jumpForceVector = randomDirection.normalized *randomJumpForce;

        playerRigid.AddForce(jumpForceVector, ForceMode2D.Impulse);
    }

    IEnumerator BounceAnimation()
    {
        BounceAnimOn = true;
        BounceEye.SetActive(true);
        yield return new WaitForSeconds(BounceAnmiationTime);
        BounceEye.SetActive(false);
        BounceAnimOn = false;

    }

    void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            PlayerBounceBack();
            if (BounceAnimOn == false)
            {
                StartCoroutine(BounceAnimation());
            }
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            PlayerBounceBack();
            if (BounceAnimOn == false)
            {
                StartCoroutine(BounceAnimation());
            }
        }
    }

    void LimitPlayerVelocity()
    {
        if (playerRigid.velocity.magnitude > playerMaxVelocity)
        {
            playerRigid.velocity = playerRigid.velocity.normalized * playerMaxVelocity;
        }
    }

    void ControlPlayerGravityScale()
    {
        if (playerRigid.velocity.y < 0)
        {
            playerRigid.gravityScale = playerFallGravityScale;
        }
        else
        {
            
            playerRigid.gravityScale =1;
        }
    }

    void HideGaugeByVelocity()
    {

        if (playerRigid.velocity.y > minJumpValue)
        {
            forceGameController.InitGauge();
            forceGameController.gameObject.SetActive(false);
        }
    }

    void CheckEndingByPosX()
    {
        if(transform.position.x> 412.75f)
        {
            gameObject.GetComponent<PlayerEndingController>().EndingPlay();
            gameObject.GetComponent<PlayerController>().enabled = false;
            Camera.main.GetComponent<CameraController>().enabled = false;
            Camera.main.transform.position = new Vector3(431.5f, 1.8f, -10);
        }
    }
    void Update()
    {
        LimitPlayerVelocity();
        PlayerJump();
        HideGaugeByVelocity();
        CheckEndingByPosX();
        // ControlPlayerGravityScale();

    }



    void FixedUpdate()
    {
        PlayerMove();
    }
}
