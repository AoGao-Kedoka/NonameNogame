using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterController : MonoBehaviour
{
    [SerializeField] private float speed, jumpSpeed;
    [SerializeField] private LayerMask ground;
    
    private PlayerInputAction _playerInputAction;
    private Rigidbody2D _rigidbody;
    private Collider2D _collider;
    private bool _faceRight = true;


    private void Awake()
    {
        _playerInputAction = new PlayerInputAction();
        _rigidbody = GetComponent<Rigidbody2D>();
        _collider = GetComponent<Collider2D>();
    }


    // Start is called before the first frame update
    void Start()
    {
        _playerInputAction.PLAYER.JUMP.started += Jump;
    }

    
    // Update is called once per frame
    void Update()
    {
        Move();
    }


    private void OnEnable()
    {
        _playerInputAction.Enable();
    }


    private void OnDisable()
    {
        _playerInputAction.PLAYER.JUMP.started -= Jump;
        _playerInputAction.Disable();
    }


    private void Move()
    {
        float val = _playerInputAction.PLAYER.MOVE.ReadValue<float>();
        Vector3 currentPosition = transform.position;
        currentPosition.x += val * speed * Time.deltaTime;

        transform.position = currentPosition;

        Flip(val);
    }


    private void Flip(float val)
    {
        if (_faceRight && val < 0 || !_faceRight && val > 0)
        {
            _faceRight = !_faceRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1;
            transform.localScale = localScale;
        }
    }


    private void Jump(InputAction.CallbackContext context)
    {
        if (isGrounded())
            _rigidbody.AddForce(new Vector2(0, jumpSpeed), ForceMode2D.Impulse);
    }


    private bool isGrounded()
    {
        Vector2 topLeft = transform.position;
        topLeft.x -= _collider.bounds.extents.x;
        topLeft.y += _collider.bounds.extents.y;

        Vector2 botRight = transform.position;
        botRight.x += _collider.bounds.extents.x;
        botRight.y -= _collider.bounds.extents.y;

        return Physics2D.OverlapArea(topLeft, botRight, ground) && _rigidbody.velocity.y == 0;
    }


    private void CheckCollider()
    {
        if (_rigidbody.velocity.y > 0)
            _collider.enabled = false;
        else
        {
            Vector2 topLeft = transform.position;
            topLeft.x -= _collider.bounds.extents.x;
            topLeft.y += _collider.bounds.extents.y;

            Vector2 botRight = transform.position;
            botRight.x += _collider.bounds.extents.x;
            botRight.y -= _collider.bounds.extents.y;
            if(!Physics2D.OverlapArea(topLeft, botRight, ground))
                _collider.enabled = true;
        }
    }
}
