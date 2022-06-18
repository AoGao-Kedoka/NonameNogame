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
    private Transform _cam;

    private void Start()
    {
        _cam = this.transform.parent;
        _startPositionX = this.transform.position.x - _cam.position.x;
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

    public void PushBackward()
    {
        // Move outside the camera and go back
        this.transform.DOMoveX(_cam.position.x - 300, 2).OnComplete(() =>
        {
            transform.DOMoveX(_startPositionX, 4);
        });
    }
}
