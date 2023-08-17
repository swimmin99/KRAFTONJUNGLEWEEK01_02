using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    public PlayerController playerController;

  
    void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            playerController.isPlayerJump = true;
            playerController.isPlayerMove = true;
        }
    }

    void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            playerController.isPlayerJump = false;
            playerController.isPlayerMove = false;
        }
    }

}
