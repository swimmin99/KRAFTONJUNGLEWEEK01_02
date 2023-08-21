using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Cinemachine;

public class PlayerController : MonoBehaviour
{

    public GameObject player;
    public Rigidbody2D playerRigid;
    public float playerFallGravityScale;
    public float playerMaxVelocity;
    public float playerMoveSpeed;
    public float playerMaxSpeed;
    public Vector2 jumpDirection;
    public bool playerOnGround;
    private bool isJumping;
    public float jumpForce;
    public float jumpLimitTime;
    public int maxJumpStep;
    public float jumpTimer;
    public int jumpLevel;
    public List <float> jumpValue = new List <float> {3.2f,3.2f,6.4f,6.4f,7.2f,7.4f,7.4f,9.3f};
    
    public float minBackBounceX;
    public float maxBackBounceX;

    public float minBackBounceY;
    public float maxBackBounceY;

    public float maxBackBounceForce;
    public float minBackBounceForce;

   
    public float minJumpValue;

    public Transform leg1;
    public Transform leg2;
    public float stepDuration = 0.1f;
    private bool isWalking = false;

    [SerializeField]
    private float BounceAnmiationTime = 0.2f;
    public GameObject BounceEye;
    private bool BounceAnimOn = false;

    public float maxVerticalAdjustment = 1f; // Maximum additional force for trajectory adjustment
    public float adjustmentSpeed = 2f; // Speed of adjustment

    public bool playerCanMove = true;


    public int MaxJumpLevel;
    public GameObject lArm;
    public GameObject rArm;
    public float targetRotation;


    private CapsuleCollider2D capsuleCollider;
    private float targetScale = 0.95f;
    private float duration = 2f;

    public CinemachineImpulseSource impSource;


    void Start()
    {
        targetRotation = 0f;
        player = gameObject;
        playerRigid = gameObject.GetComponent<Rigidbody2D>();



        capsuleCollider = GetComponent<CapsuleCollider2D>();
        Sequence sequence = DOTween.Sequence();
        sequence.Append(DOTween.To(() => capsuleCollider.size, newSize => capsuleCollider.size = newSize, capsuleCollider.size * targetScale, duration));
        sequence.OnComplete(() => RestoreOriginalScale());
        sequence.SetLoops(-1);

    }

    private void RestoreOriginalScale()
    {
        capsuleCollider.transform.localScale = Vector3.one;
    }


void PlayerMove()
    {
        if (playerOnGround)
        {
            if (playerCanMove)
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

                if (isWalking == false && Mathf.Abs(playerRigid.velocity.x) > 0.2f)
                {
                    MoveLeg(leg1, leg2, 10f);
                }
            }
        }
        
    }

    private void MoveLeg(Transform leg1, Transform leg2, float MoveAngleAbs)
    {
        isWalking = true;
        leg1.DOLocalRotate(new Vector3(0f, 0f, MoveAngleAbs), stepDuration).OnComplete(() => leg1.DOLocalRotate(new Vector3(0f, 0f, -MoveAngleAbs), stepDuration).OnComplete(() => isWalking = false));
        leg2.DOLocalRotate(new Vector3(0f, 0f, -MoveAngleAbs), stepDuration).OnComplete(() => leg2.DOLocalRotate(new Vector3(0f, 0f, MoveAngleAbs), stepDuration).OnComplete(() => isWalking = false));
    }

    public void CancelJump()
    {
        playerOnGround = false;
        playerCanMove = true;
        jumpTimer = 0;
        jumpLevel = 0;
    }



    void PlayerJump()
    {
        if (playerOnGround)
        {
            if (Input.GetButtonDown("Jump"))
            {
                print("Detected");
                isJumping = true;
                playerCanMove = false;
            }

            if (Input.GetButton("Jump") && isJumping)
            {
                
                    jumpTimer += Time.deltaTime;
                    print(jumpTimer);
                    jumpTimer = Mathf.Clamp(jumpTimer, 0, 1.5f);
                    jumpLevel = (int)(jumpTimer * MaxJumpLevel);
                    print(jumpLevel);

                    switch (jumpLevel)
                    {
                        case 0: targetRotation = 15f; break;
                        case 1: targetRotation = 15f;break;
                        case 2: targetRotation = 45f; break;
                        case 3: targetRotation = 45f; break;
                        case 4: targetRotation = 65f; break;
                        case 5: targetRotation = 65f; break;
                        case 6: targetRotation = 65f; break;
                        case 7: targetRotation = 90f; break;
                    }

            }

            if (Input.GetButtonUp("Jump") && isJumping)
            {
                print("up");
                playerCanMove = true;
                targetRotation = 0f;
                isJumping = false;
                print(jumpLevel);
                Vector2 jumpForceVector = jumpDirection.normalized * jumpValue[jumpLevel];
                playerRigid.AddForce(jumpForceVector, ForceMode2D.Impulse);
                jumpTimer = 0;
                jumpLevel = 0;
            }
        }

    }

    void PlayerBounceBack(bool animationOnly)
    {
        impSource.GenerateImpulse(0.1f);

        CancelJump();
        if (animationOnly == false)
        {
            float randomDirectionX = Random.Range(minBackBounceX, maxBackBounceX);
            float randomDirectionY = Random.Range(minBackBounceY, maxBackBounceY);
            Vector2 randomDirection = new Vector2(randomDirectionX, randomDirectionY);
            float randomJumpForce = Random.Range(minBackBounceForce, maxBackBounceForce);
            Debug.Log("nowForce : " + randomJumpForce);
            Vector2 jumpForceVector = randomDirection.normalized * randomJumpForce;
            playerRigid.AddForce(jumpForceVector, ForceMode2D.Impulse);


        }
        impSource.GenerateImpulse(0.1f);
    }

  


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;

        Gizmos.DrawLine(transform.position + new Vector3(minBackBounceX, minBackBounceX), transform.position);

    }


    public void playAnimation()
    {
        StartCoroutine(BounceAnimation());
    }

    IEnumerator BounceAnimation()
    {
        BounceAnimOn = true;
        BounceEye.SetActive(true);
        yield return new WaitForSeconds(BounceAnmiationTime);
        BounceEye.SetActive(false);
        BounceAnimOn = false;

    }

   


    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            PlayerBounceBack(false);
            if (BounceAnimOn == false)
            {
                StartCoroutine(BounceAnimation());
            }
        } else if (other.gameObject.CompareTag("TargetBouncer"))
        {
            PlayerBounceBack(true);
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

    void rotateArm()
    {
        
        lArm.transform.localRotation = Quaternion.Euler(0f, 0f, -1f*targetRotation);
        rArm.transform.localRotation = Quaternion.Euler(0f, 0f, targetRotation);

    }

   
    void Update()
    {
        LimitPlayerVelocity();
        PlayerJump();

    }

    void TweenCollider()
    {
        
    }

    void FixedUpdate()
    {
        PlayerMove();
        rotateArm();

    }

}
