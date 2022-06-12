using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class JumpThroughFloating : MonoBehaviour
{
    [SerializeField] private Collider2D playerCollider;

    private GameObject _platform;
    private PlayerInputAction _playerInputAction;


    private void Awake()
    {
        _playerInputAction = new PlayerInputAction();
    }


    private void Start()
    {
        _playerInputAction.PLAYER.FALL.started += Fall;
    }


    private void OnEnable()
    {
        _playerInputAction.Enable();
    }


    private void OnDisable()
    {
        _playerInputAction.PLAYER.FALL.started -= Fall;
        _playerInputAction.Disable();
    }


    private void Fall(InputAction.CallbackContext context)
    {
        if (_platform != null)
            StartCoroutine(DisableCollision());
    }


    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Floating"))
            _platform = col.gameObject;
    }


    private void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Floating"))
            _platform = null;
    }


    private IEnumerator DisableCollision()
    {
        Collider2D collider = _platform.GetComponent<Collider2D>();
        
        Physics2D.IgnoreCollision(playerCollider, collider);
        yield return new WaitForSeconds(1f);
        Physics2D.IgnoreCollision(playerCollider, collider, false);
    }
}
