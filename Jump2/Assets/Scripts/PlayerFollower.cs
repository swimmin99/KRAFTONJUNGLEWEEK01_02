using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFollower : MonoBehaviour
{
    public GameObject player;
    private void FixedUpdate()
    {
        transform.position = player.transform.position;
    }
}
