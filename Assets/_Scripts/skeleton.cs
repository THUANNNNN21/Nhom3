using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(TouchingAnimation), typeof(Damageable))]
public class skeleton : MonoBehaviour
{
    private bool hasFlipped = false;
    public float walkSpeed = 5f;
    public DetectionZone AttackZone;
    Rigidbody2D rb;
    public enum WalkableDirection { Right, Left }
    private WalkableDirection _WalkDirection;
    private Vector2 walkdirectionVector = Vector2.right;
    TouchingAnimation tc;
    Animator animator;
    Damageable damageable;
    public WalkableDirection WalkDirection
    {
        get { return _WalkDirection; }
        set
        {
            if (_WalkDirection != value)
            {
                //đổi hướng
                gameObject.transform.localScale = new Vector2(gameObject.transform.localScale.x * -1, gameObject.transform.localScale.y);
                if (value == WalkableDirection.Right)
                {
                    walkdirectionVector = Vector2.right;
                }
                else if (value == WalkableDirection.Left)
                {
                    walkdirectionVector = Vector2.left;
                }
                _WalkDirection = value;
            }
        }

    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        damageable = GetComponent<Damageable>();
        tc = GetComponent<TouchingAnimation>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }
    public bool _hasTarget = false;
    public bool HasTarget
    {
        get { return _hasTarget; }
        private set
        {
            _hasTarget = value;
            animator.SetBool(AnimationString.hasTarget, value);
        }
    }
    // Update is called once per frame
    void Update()
    {
        HasTarget = AttackZone.detectiveColliders.Count > 0;
    }
    public float WalkStopRate = 1;
    void FixedUpdate()
    {
        if (!hasFlipped && tc.IsGrounded && tc.IsOnWall)
        {
            FlipDirection();
            hasFlipped = true;
        }
        else if (!tc.IsGrounded || !tc.IsOnWall)
        {
            hasFlipped = false;
        }
        if (!damageable.LockVelocity)
        {
            if (CanMove)
            {
                Vector2 newVelocity = rb.linearVelocity;
                newVelocity.x = walkSpeed * walkdirectionVector.x;
                rb.linearVelocity = newVelocity;
            }
            else
            {
                Vector2 newVelocity = rb.linearVelocity;
                newVelocity.x = Mathf.Lerp(walkdirectionVector.x, 0, WalkStopRate);
                rb.linearVelocity = newVelocity;
            }
        }
    }
    private void FlipDirection()
    {
        if (WalkDirection == WalkableDirection.Right)
        {
            WalkDirection = WalkableDirection.Left;
        }
        else if (WalkDirection == WalkableDirection.Left)
        {
            WalkDirection = WalkableDirection.Right;
        }
        else
            Debug.Log("aa");
    }
    public bool CanMove
    {
        get { return animator.GetBool(AnimationString.canMove); }
    }
    public void OnHit(int damage, Vector2 knockback)
    {
        rb.linearVelocity = new Vector2(knockback.x, rb.linearVelocityY + knockback.y);
    }
}
