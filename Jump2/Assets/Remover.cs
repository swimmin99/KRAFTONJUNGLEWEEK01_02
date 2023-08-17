using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Remover : MonoBehaviour
{
    [SerializeField]
    private float deleteTime;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Removing());
    }

    IEnumerator Removing()
    {
        yield return new WaitForSeconds(deleteTime);
        Destroy(gameObject);
    }
}