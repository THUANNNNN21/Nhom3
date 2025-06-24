using System;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D), typeof(TouchingAnimation), typeof(Damageable))]
public class PlayerController : MonoBehaviour
{
    //biến di chuyển
    Vector2 MoveInput;
    public float WalkSpeed = 5;
    public float RunSpeed = 8;
    public float jumpImpulse = 10f;
    public float airSpeed = 3f;
    private bool isBoosted = false;
    //tốc độ di chuyển
    public float CurrentSpeed
    {
        get
        {
            if (CanMove)
            {
                if (IsMoving && !tc.IsOnWall)
                {
                    if (tc.IsGrounded)
                    {
                        if (IsRunning)
                        {
                            return RunSpeed;
                        }
                        else
                        {
                            return WalkSpeed;
                        }
                    }
                    else
                    //air Speed
                    {
                        return airSpeed;
                    }
                }
                else
                {
                    return 0;
                }
            }
            else
            {
                return 0;
            }
        }
    }
    Damageable damageable;
    //animator
    Animator animator;
    TouchingAnimation tc;
    [SerializeField]
    private bool _isMoving = false;
    public bool IsMoving
    {
        get
        {
            return _isMoving;
        }
        private set
        {
            _isMoving = value;
            animator.SetBool(AnimationString.isMoving, value);
        }
    }
    //ani chạy
    [SerializeField]
    private bool _isRunning = false;
    public bool IsRunning
    {
        get
        {
            return _isRunning;
        }
        private set
        {
            _isRunning = value;
            animator.SetBool(AnimationString.isRunning, value);
        }
    }

    private bool _IsFacingRight = true;
    //biến ani phương hướng nhìn
    public bool IsFacingRight
    {
        get
        {
            return _IsFacingRight;
        }
        private set
        {
            if (_IsFacingRight != value)
            {
                transform.localScale *= new Vector2(-1, 1);
            }
            _IsFacingRight = value;
        }
    }

    //
    Rigidbody2D rb;
    //biến ko di chuyển khi tấn công
    public bool CanMove
    {
        get { return animator.GetBool(AnimationString.canMove); }
    }
    public bool isAlive
    {
        get { return animator.GetBool(AnimationString.isAlive); }
    }
    void Awake()
    {
        damageable = GetComponent<Damageable>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        tc = GetComponent<TouchingAnimation>();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private attack attackScript;
    void Start()
    {
        Transform attackObject = transform.Find("SwordAttack");
        if (attackObject != null)
        {
            attackScript = attackObject.GetComponent<attack>();
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        //nhân vật di chuyển
        if (!damageable.LockVelocity)
            rb.linearVelocity = new Vector2(MoveInput.x * CurrentSpeed, rb.linearVelocity.y);
        animator.SetFloat(AnimationString.yVelocity, rb.linearVelocityY);
    }
    public void OnMove(InputAction.CallbackContext context)
    {
        //lấy input di chuyển
        MoveInput = context.ReadValue<Vector2>();
        if (isAlive)
        {
            IsMoving = MoveInput != Vector2.zero;
            //gọi pthuc hướng nhìn của nhân vật theo trục x
            IsFacingDirection(MoveInput);
        }
        else IsMoving = false;
    }

    //Phương thức tạo hướng 
    private void IsFacingDirection(Vector2 moveInput)
    {
        //nếu x>0 và ko nhìn phải-> nhìn phải
        if (MoveInput.x > 0 && !IsFacingRight)
        {
            IsFacingRight = true;
        }
        //nếu nhìn phải, x<0->nhìn trái
        else if (MoveInput.x < 0 && IsFacingRight)
        {
            IsFacingRight = false;
        }
    }
    public void OnRun(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            IsRunning = true;
        }
        else if (context.canceled)
        {
            IsRunning = false;
        }
    }
    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.started && tc.IsGrounded && CanMove)
        {
            animator.SetTrigger(AnimationString.jumpTrigger);
            rb.linearVelocity = new Vector2(rb.linearVelocityX, jumpImpulse);
        }
    }
    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            animator.SetTrigger(AnimationString.attackTrigger);
        }
    }
    public void OnHit(int damage, Vector2 knockback)
    {
        rb.linearVelocity = new Vector2(knockback.x, rb.linearVelocityY + knockback.y);
    }
    public void OnRangedAttack(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            animator.SetTrigger(AnimationString.rangedattackTrigger);
        }
    }
    public void OnBoost(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            if (!isBoosted)
            {
                WalkSpeed *= 1.5f;
                RunSpeed *= 1.5f;
                jumpImpulse *= 1.2f;
                airSpeed *= 1.5f;

                if (attackScript != null)
                {
                    attackScript.attackDamage /= 2;
                }
                if (animator != null)
                {
                    animator.SetTrigger(AnimationString.boostTrigger);
                }

                isBoosted = true;
                Debug.Log("Boost ON");
            }
            else
            {
                WalkSpeed /= 1.5f;
                RunSpeed /= 1.5f;
                jumpImpulse /= 1.2f;
                airSpeed /= 1.5f;

                if (attackScript != null)
                {
                    attackScript.attackDamage *= 2;
                }
                if (animator != null)
                {
                    animator.SetTrigger(AnimationString.boostTrigger);
                }
                isBoosted = false;
                Debug.Log("Boost OFF");
            }
        }
    }

}
