using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ForceGameController : MonoBehaviour
{
    public Vector3 guageStartPos;
    public Vector3 guageEndPos;
    public PlayerController playerController;
    public Transform gaugeFilling;

    public void IncreaseSpeedGauge(float jumpLimitTime)
    {
        gaugeFilling.DOLocalMove(guageEndPos, jumpLimitTime);
    }
    /*
        public void IncreaseSpeedGauge()
        {
            Vector3 gaugeWorldPos= Vector3.Lerp(guageStartPos, guageEndPos, playerController.jumpTimer);
            Vector3 playerLocalPos= playerController.transform.InverseTransformPoint(gaugeWorldPos);
            gaugeFilling.position = playerLocalPos;
        }*/

    public void InitGauge()
    {
        gaugeFilling.DOKill();
        gaugeFilling.localPosition = guageStartPos;
    }

}
