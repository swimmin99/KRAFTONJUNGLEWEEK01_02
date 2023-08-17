using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public Transform player;

    void Update()
    {
        Vector3 cameraPos = new Vector3(player.position.x, player.position.y+3f, -10);
        transform.position = cameraPos;
    }
}
