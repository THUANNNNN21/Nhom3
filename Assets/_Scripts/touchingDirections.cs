using Unity.VisualScripting;
using UnityEngine;

public class touchingDirections : MonoBehaviour
{
    Rigidbody2D rb;
    Animator animator;
    CapsuleCollider2D touching;
    public ContactFilter2D contactFilter;
    RaycastHit2D[] groundHits = new RaycastHit2D[5];
    public float groundDistance = 0.05f;
    public bool _isGrounded;
    
    public bool isGrounded
    {
        get
        {
            return _isGrounded;
        }
        set
        {
            _isGrounded = value;
            animator.SetBool("isGrounded", value);
        }
    }

    public void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        touching = GetComponent<CapsuleCollider2D>();
        animator = GetComponent<Animator>();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        isGrounded = touching.Cast(Vector2.down, contactFilter, groundHits, groundDistance) > 0;
    }
}
