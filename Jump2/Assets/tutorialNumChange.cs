using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class tutorialNumChange : MonoBehaviour
{
    TextMeshProUGUI textMeshProGUI;
    public float minChangeInterval = 0.5f;
    public float maxChangeInterval = 10.0f;
    private bool isOne = false;

    private void Start()
    {
        textMeshProGUI = GetComponent<TextMeshProUGUI>();
        
        StartCoroutine(ChangeTextCoroutine());
    }

    private IEnumerator ChangeTextCoroutine()
    {
        yield return new WaitForSeconds(40f);
        while (true)
        {
            if (isOne)
            {
                textMeshProGUI.text = "?";
            }
            else
            {
                textMeshProGUI.text = "1";
            }

            isOne = !isOne;

            float waitTime = Random.Range(minChangeInterval, maxChangeInterval);
            yield return new WaitForSeconds(waitTime);
        }
    }
}
