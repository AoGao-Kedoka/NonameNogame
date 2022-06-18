using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class AttackingRobotController : MonoBehaviour
{
    [SerializeField] private int loopDuration;
    private void Start()
    {
        this.transform.DOLocalMoveX(transform.position.x + 20, loopDuration)
            .SetEase(Ease.InOutSine).SetLoops(-1, LoopType.Yoyo);
    }

    public void GetHacked()
    {
       //TODO 
    }
}
