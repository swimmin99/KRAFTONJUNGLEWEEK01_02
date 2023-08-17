using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Instantiater : MonoBehaviour
{
    [SerializeField]
    private float instantiatingTime;
    [SerializeField]
    private GameObject prefab;

    private void Start()
    {
        StartCoroutine(Instantiate());
    }

    IEnumerator Instantiate()
    {
        while (true)
        {
            yield return new WaitForSeconds(instantiatingTime);
            Instantiate(prefab, transform.position, transform.rotation);
        }
    }
}