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

    private void Awake()
    {
        _playerInputAction = new PlayerInputAction();
    }

    private void OnEnable()
    {
        _playerInputAction.Enable();
    }

  /*  private void OnDisable()
    {
        _playerInputAction.Disable();
        _playerInputAction.PLAYER.DEBUG.started -= GetOverride;
    }
    private void Start()
    {
        _playerInputAction.PLAYER.DEBUG.started += GetOverride;
        _chasingRobot = GameObject.Find("ChasingRobot");
    }*/

    private void Update()
    {
        if (overrided)
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
        if (collision.CompareTag("ChasingRobot") && overrided == true)
        {
            Debug.Log("DeBUG");
            collision.GetComponent<ChasingRobotController>().PushedBack();
        }
    }
    
}
