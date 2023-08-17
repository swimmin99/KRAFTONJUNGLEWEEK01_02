using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    public PlayerController playerController;

    public float raycastStartPosition = 1f;
    public float raycastAmount = 1f;
    public float raycastDistance = 1f;
    private void Update()
    {
        LayerMask groundLayer = LayerMask.GetMask("Ground");

        RaycastHit2D hitLeft = Physics2D.Raycast(new Vector3(transform.position.x - raycastDistance, transform.position.y - raycastStartPosition, transform.position.z), Vector2.down, raycastDistance, groundLayer);
        RaycastHit2D hitRight = Physics2D.Raycast(new Vector3(transform.position.x + raycastDistance, transform.position.y - raycastStartPosition, transform.position.z), Vector2.down, raycastDistance, groundLayer);

        if (hitLeft.collider == null && hitRight.collider == null)
        {
            playerController.playerOnGround = false;
        }
        else
        {
            playerController.playerOnGround = true;
        }

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;

        Gizmos.DrawLine(new Vector3(transform.position.x - raycastDistance, transform.position.y - raycastStartPosition, transform.position.z), new Vector3(transform.position.x - raycastDistance, transform.position.y - raycastStartPosition - raycastAmount, transform.position.z));
        Gizmos.DrawLine(new Vector3(transform.position.x + raycastDistance, transform.position.y - raycastStartPosition, transform.position.z), new Vector3(transform.position.x + raycastDistance, transform.position.y - raycastStartPosition - raycastAmount, transform.position.z));

    }


}
