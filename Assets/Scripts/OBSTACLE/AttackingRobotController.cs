using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine.InputSystem;
using UnityEngine;

public class AttackingRobotController : Obstacle
{
    [SerializeField] private float _overrideSpeed;
    private GameObject _chasingRobot;
    private PlayerInputAction _playerInputAction;
    private bool overrided = false;


    private void Start()
    {
        _chasingRobot = GameObject.Find("ChasingRobot");
    }

    private void Update()
    {
        if (overriden)
        {
            transform.position -= new Vector3(_overrideSpeed, 0, 0) * Time.deltaTime;
        }
    }

   // public void GetOverride(InputAction.CallbackContext context)
    //{
      //  overrided = true;
    //}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("ChasingRobot") && overriden == true)
        {
            Debug.Log("DeBUG");
            collision.GetComponent<ChasingRobotController>().PushedBack();
        }
    }
    
}
