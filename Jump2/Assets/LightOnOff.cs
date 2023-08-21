using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class LightOnOff : MonoBehaviour
{
    [SerializeField]
    private float onTime;
    [SerializeField]
    private float offTime;
    [SerializeField]
    CinemachineVirtualCamera targetCamera;

    PolygonCollider2D myCollider;

    [SerializeField]
    GameObject graphics;
    
    // Start is called before the first frame update
    void Start()
    {
        myCollider = GetComponent<PolygonCollider2D>();
   
        StartCoroutine(showOff());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        targetCamera.Follow = transform;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        targetCamera.Follow = GameObject.FindGameObjectWithTag("Player").transform;
    }


    IEnumerator showOff ()
    {
        while (true) {
            yield return new WaitForSeconds(.1f);
            graphics.SetActive(false);
            yield return new WaitForSeconds(.1f);
            graphics.SetActive(true);
            yield return new WaitForSeconds(onTime);
            graphics.SetActive(false);

            yield return new WaitForSeconds(.1f);
            graphics.SetActive(true);
            yield return new WaitForSeconds(.1f);
            graphics.SetActive(false);
            yield return new WaitForSeconds(offTime);
            graphics.SetActive(true);


        }
    }
}
