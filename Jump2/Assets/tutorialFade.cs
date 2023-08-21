using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
public class tutorialFade : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI text01;
    [SerializeField]
    TextMeshProUGUI text02;

    [Header("Fading Settings")]
    public float fadeDuration = 1f;
    public float pauseDuration = 1f;

    private void Start()
    {
        StartFading();
    }

    private void StartFading()
    {
        Sequence fadeSequence = DOTween.Sequence();

        fadeSequence.Append(text01.DOFade(0f, fadeDuration));
        fadeSequence.Join(text02.DOFade(0f, fadeDuration));
        fadeSequence.AppendInterval(pauseDuration);
        fadeSequence.Append(text01.DOFade(0.5f, fadeDuration));
        fadeSequence.Join(text02.DOFade(0.5f, fadeDuration));
        fadeSequence.AppendInterval(pauseDuration);
        fadeSequence.Append(text01.DOFade(0f, fadeDuration));
        fadeSequence.Join(text02.DOFade(0f, fadeDuration));
        fadeSequence.AppendInterval(pauseDuration);
        fadeSequence.SetLoops(-1, LoopType.Restart);
    }
}