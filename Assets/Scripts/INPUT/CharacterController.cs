using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using DG.Tweening;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class CharacterController : MonoBehaviour
{
    [SerializeField] private float speed, jumpSpeed;
    [SerializeField] private LayerMask ground;

    private PlayerInputAction _playerInputAction;
    private Rigidbody2D _rigidbody;
    private Collider2D _collider;

    private bool _faceRight = true;
    private bool _doubleJump;
    private bool _canDash = true;
    private bool _isDashing;

    private float _coyoteTime = .25f;
    private float _coyoteTimeCounter;
    [SerializeField] private float _dashingPower = 24f;
    [SerializeField] private float _dashingTime = 0.2f;
    [SerializeField] private float _dashingCooldown = 1f;

    [SerializeField] private ProgressBar progressBar;
    [SerializeField] private ChasingRobotController chasingRobot;

    private Animator _animator;
    private List<GameObject> inrangeObstacles = new List<GameObject>();
    private bool _canOverride = false;

    private Vector3 _startPosition;

    private void Awake()
    {
        _playerInputAction = new PlayerInputAction();
        _rigidbody = GetComponent<Rigidbody2D>();
        _collider = GetComponent<Collider2D>();
        _animator = GetComponent<Animator>();
        _startPosition = transform.position;
    }


    // Start is called before the first frame update
    void Start()
    {
        _playerInputAction.PLAYER.JUMP.started += Jump;
        _playerInputAction.PLAYER.DASH.started += Dash;
        _playerInputAction.PLAYER.OVERRIDE.started += Override;
    }


    // Update is called once per frame
    void Update()
    {
        if (_isDashing)
            return;

        //Move();
        if (isGrounded())
        {
            _coyoteTimeCounter = _coyoteTime;
            _doubleJump = false;
            _animator.SetBool("jump", false);
        }
        else
        {
            _coyoteTimeCounter -= Time.deltaTime;
        }
    }


    private void FixedUpdate()
    {
        if (_isDashing)
            return;

        Move();
    }


    private void OnEnable()
    {
        _playerInputAction.Enable();
    }


    private void OnDisable()
    {
        _playerInputAction.PLAYER.JUMP.started -= Jump;
        _playerInputAction.PLAYER.DASH.started -= Dash;
        _playerInputAction.PLAYER.OVERRIDE.started -= Override;
        _playerInputAction.Disable();
    }


    private void Move()
    {
        float val = _playerInputAction.PLAYER.MOVE.ReadValue<float>();
        // Vector3 currentPosition = transform.position;
        // currentPosition.x += val * speed * Time.deltaTime;
        //
        // transform.position = currentPosition;

        if (val > 0 || val < 0)
        {
            _animator.SetBool("running", true);
        }
        else
        {
            _animator.SetBool("running", false);
        }
        _rigidbody.velocity = new Vector2(val * speed, _rigidbody.velocity.y);

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
        if (_isDashing)
            return;

        if (_coyoteTimeCounter > 0f || _doubleJump)
        {
            _animator.SetBool("jump", true);
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, jumpSpeed);
            _doubleJump = !_doubleJump;
            _coyoteTimeCounter = 0f;
        }
    }


    private void Dash(InputAction.CallbackContext context)
    {
        if (_canDash)
        {
            _animator.SetTrigger("dash");
            StartCoroutine(Dash());
        }
    }


    private void Override(InputAction.CallbackContext context)
    {
        if (_canOverride)
        {
            progressBar.StartOverride(inrangeObstacles);
            _canOverride = false;
            _animator.SetTrigger("smash");
        }
    }


    public void InOverrideRange(GameObject obj)
    {
        inrangeObstacles.Add(obj);
        _canOverride = true;
    }


    public void LeftOverrideRange(GameObject obj)
    {
        inrangeObstacles.Remove(obj);
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
            if (!Physics2D.OverlapArea(topLeft, botRight, ground))
                _collider.enabled = true;
        }
    }

    private IEnumerator Dash()
    {
        _canDash = false;
        _isDashing = true;

        float originalGravity = _rigidbody.gravityScale;
        _rigidbody.gravityScale = 0f;
        _rigidbody.velocity = new Vector2(transform.localScale.x * _dashingPower, 0f);

        yield return new WaitForSeconds(_dashingTime);

        _rigidbody.gravityScale = originalGravity;
        _isDashing = false;

        yield return new WaitForSeconds(_dashingCooldown);

        _canDash = true;
    }


    public void Die()
    {
        StartCoroutine(DieAndRespawn());
    }


    private IEnumerator DieAndRespawn()
    {
        //Play Dead Animation
        chasingRobot.chasingSpeed = 0f;
        _animator.SetBool("dead", true);

        yield return new WaitForSeconds(2.5f);

        //Respawn at last Checkpoint, wait at the same position 1 seconds, and then respone
        var currentScene = SceneManager.GetActiveScene();
        this._playerInputAction.Disable();
        this.transform.DOMoveX(transform.position.x, 1).OnComplete(() =>
        {
            // reset the whole scene
            SceneManager.LoadScene(currentScene.name);
        });
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        //TODO: add more 
        if (collision.gameObject.CompareTag("Door"))
        {
            Debug.Log("Load Main Scene");
            SceneManager.LoadScene("Level");
        }
        if (collision.gameObject.CompareTag("End"))
        {
            Debug.Log("Load End Scene");
            SceneManager.LoadScene("End");
        }
        List<String> tags = new List<String> { "ChasingRobot" };
        foreach (var tag in tags)
        {
            if (collision.gameObject.CompareTag(tag))
            {
                Die();
            }
        }
    }
}
