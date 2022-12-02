using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public PlayerData Data;
    #region COMPONENTS
    public Rigidbody2D _rb { get; private set; }
    #endregion

    #region STATE PARAMETERS
    public bool IsJumping { get; private set; }

    // Timers
    private float _lastOnGroundTime;    

    // Jump
    private bool _isJumpCut;
    private bool _isJumpFalling;

    #endregion

    #region INPUT
    private Vector2 _moveInput;
    public float _lastPressedJumpTime { get; private set; }
    #endregion

    #region CHECK PARAMETERS
    [Header("Checks")]
    [SerializeField] private Transform _groundCheckPoint;

    [SerializeField] private Vector2 _groundCheckSize = new Vector2(0.5f, 0.03f);
    #endregion

    #region LAYERS & TAGS
    [Header("Layers & Tags")]
    [SerializeField] private LayerMask _groundLayer;
    #endregion

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }
    void Start()
    {
        SetGravityScale(Data.gravityScale);
    }

    // Update is called once per frame
    void Update()
    {
        #region TIMERS
        _lastOnGroundTime -= Time.deltaTime;
        _lastPressedJumpTime -= Time.deltaTime;
        #endregion

        #region INPUT HANDLER
        _moveInput.x = Input.GetAxisRaw("Horizontal");
        _moveInput.y = Input.GetAxisRaw("Vertical");

        if (Input.GetKeyDown(KeyCode.Space))
        {
            OnJumpInput();
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            OnJumpUpInput();
        }
        #endregion

        #region COLLISION CHECKS
        if (!IsJumping)
        {
            if (Physics2D.OverlapBox(_groundCheckPoint.position, _groundCheckSize, 0, _groundLayer) && !IsJumping)
            {
                _lastOnGroundTime = Data.coyoteTime;
            }
        }
        #endregion

        #region JUMP CHECKS
        // Falling down
        if (IsJumping && _rb.velocity.y < 0)
        {
            IsJumping = false;
        }
        // Too late for jump cut
        if (_lastOnGroundTime > 0 && !IsJumping)
        {
            _isJumpCut = false;

            if (!IsJumping)
                _isJumpFalling = false;
        }

        // Jump
        if (CanJump() && _lastPressedJumpTime > 0)
        {
            IsJumping = true;
            _isJumpCut = false;
            _isJumpFalling = false;
            Jump();
        }
        #endregion

        #region GRAVITY
        if (_rb.velocity.y < 0 && _moveInput.y < 0)
        {
            // Much higher gravity if holding down
            SetGravityScale(Data.gravityScale * Data.fastFallGravityMult);
            // Caps maximum fall speed
            _rb.velocity = new Vector2(_rb.velocity.x, Mathf.Max(_rb.velocity.y, -Data.maxFastFallSpeed));
        }
        else if (_isJumpCut)
        {
            // Higher gravity if jump button released
            SetGravityScale(Data.gravityScale * Data.jumpCutGravityMult);
            _rb.velocity = new Vector2(_rb.velocity.x, Mathf.Max(_rb.velocity.y, -Data.maxFallSpeed));
        }
        else if ((IsJumping || _isJumpFalling) && Mathf.Abs(_rb.velocity.y) < Data.jumpHangTimeThreshold)
        {
            // Smaller gravity when close to the apex
            SetGravityScale(Data.gravityScale * Data.jumpHangGravityMult);
        }
        else if (_rb.velocity.y < 0)
        {
            // Higher gravity if falling
            SetGravityScale(Data.gravityScale * Data.fallGravityMult);
            // Caps maximum fall speed
            _rb.velocity = new Vector2(_rb.velocity.x, Mathf.Max(_rb.velocity.y, -Data.maxFallSpeed));
        }
        else
        {
            // Default gravity
            SetGravityScale(Data.gravityScale);
        }
        #endregion

    }
    private void FixedUpdate()
    {
        Run();
    }

    #region INPUT CALLBACKS
    public void OnJumpInput()
    {
        _lastPressedJumpTime = Data.jumpInputBufferTime;
    }
    public void OnJumpUpInput()
    {
        if (CanJumpCut())
            _isJumpCut = true;
    }
    #endregion

    #region RUN METHODS
    private void Run()
    {
        float targetSpeed = _moveInput.x * Data.runMaxSpeed;
        targetSpeed = Mathf.Lerp(_rb.velocity.x, targetSpeed, 1);

        #region Calculate AccelRate
        float accelRate;

        //Gets an acceleration value based on if we are accelerating (includes turning) 
        //or trying to decelerate (stop). As well as applying a multiplier if we're air borne.
        if (_lastOnGroundTime > 0)
            accelRate = (Mathf.Abs(targetSpeed) > 0.01f) ? Data.runAccelAmount : Data.runDecceleration;
        else
            accelRate = (Mathf.Abs(targetSpeed) > 0.01f) ? Data.runDeccelAmount * Data.accelInAir : Data.runDecceleration * Data.deccelInAir;
        #endregion

        if ((IsJumping || _isJumpFalling) && Mathf.Abs(_rb.velocity.y) < Data.jumpHangTimeThreshold)
        {
            accelRate *= Data.jumpHangAccelerationMult;
            targetSpeed *= Data.jumpHangMaxSpeedMult;
        }

        float speedDif = targetSpeed - _rb.velocity.x;

        float movement = speedDif * accelRate;

        _rb.AddForce(movement * Vector2.right, ForceMode2D.Force);
    }
    #endregion

    #region JUMP METHODS
    private void Jump()
    {
        _lastPressedJumpTime = 0;
        _lastOnGroundTime = 0;

        float force = Data.jumpForce;
        if (_rb.velocity.y < 0)
            force -= _rb.velocity.y;

        _rb.AddForce(Vector2.up * force, ForceMode2D.Impulse);
    }
    #endregion

    #region GENERAL METHODS
    private void SetGravityScale(float scale)
    {
        _rb.gravityScale = scale;
    }
    #endregion

    #region CHECK METHODS
    private bool CanJump()
    {
        return _lastOnGroundTime > 0 && !IsJumping;
    }
    private bool CanJumpCut()
    {
        return IsJumping && _rb.velocity.y > 0;
    }
    #endregion

    private void OnDrawGizmos()
    {
        /*Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, new Vector3(1, 1, 1));*/

        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(_groundCheckPoint.position, _groundCheckSize);
    }
}
