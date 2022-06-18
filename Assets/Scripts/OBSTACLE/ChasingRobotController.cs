using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.InputSystem;

public class ChasingRobotController : MonoBehaviour
{
    [SerializeField] private float chasingSpeed;

    private float _startPositionX;

    private void Start()
    {
        _startPositionX = this.transform.position.x - this.transform.parent.position.x;

    }

    // Update is called once per frame
    void Update()
    {
       MoveForward(); 
    }

    void MoveForward()
    {
        this.transform.position += new Vector3(chasingSpeed, 0, 0) * Time.deltaTime;
    }

    void PushBackward()
    {
        this.transform.DOMoveX(this.transform.parent.position.x + _startPositionX, 1);
    }
}
