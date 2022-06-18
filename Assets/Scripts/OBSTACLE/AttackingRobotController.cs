using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine.InputSystem;
using UnityEngine;

public class AttackingRobotController : MonoBehaviour
{
    [SerializeField] private float _overrideSpeed;
    private GameObject _chasingRobots;
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

    private void OnDisable()
    {
        _playerInputAction.Disable();
        _playerInputAction.PLAYER.DEBUG.started -= GetOverride;
    }
    private void Start()
    {
        _playerInputAction.PLAYER.DEBUG.started += GetOverride;
        _chasingRobots = GameObject.Find("ChasingRobot");
    }

    private void Update()
    {
        if (overrided)
        {
            transform.position -= new Vector3(_overrideSpeed, 0, 0);
        }
    }

    public void GetOverride(InputAction.CallbackContext context)
    {
        Debug.Log("Override called");
        overrided = true;
        Physics2D.IgnoreLayerCollision(this.gameObject.layer, _chasingRobots.gameObject.layer, true);
    }
}
