using System;
using System.Collections.Generic;
using UnityEngine;

public class Bat : MonoBehaviour
{
    public float flySpeed = 20f;
    public float waypointReachedDistance = 0.1f;
    public List<Transform> waypoints;
    public DetectionZone biteZone;
    Animator animator;
    Rigidbody2D rb;
    Damageable damageable;
    Transform nextWayPoint;
    int wayPointNum = 0;
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
    public bool CanMove
    {
        get { return animator.GetBool(AnimationString.canMove); }
    }
    private void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        damageable = GetComponent<Damageable>();
    }
    void Start()
    {
        nextWayPoint = waypoints[wayPointNum];
    }
    // Update is called once per frame
    void Update()
    {
        HasTarget = biteZone.detectiveColliders.Count > 0;
    }
    private void FixedUpdate()
    {
        if (damageable.IsAlive)
        {
            if (CanMove)
            {
                Flight();
            }
            else
            {
                rb.linearVelocity = Vector3.zero;
            }
        }
        else
        {
            rb.gravityScale = 2f;
        }
    }
    private void Flight()
    {
        Vector2 directionToWayPoint = (nextWayPoint.position - transform.position).normalized;
        float distance = Vector2.Distance(nextWayPoint.position, transform.position);
        rb.linearVelocity = directionToWayPoint * flySpeed;
        UpdateDirection();
        if (distance <= waypointReachedDistance)
        {
            wayPointNum++;
            if (wayPointNum >= waypoints.Count)
            {
                wayPointNum = 0;
            }
            nextWayPoint = waypoints[wayPointNum];
        }
    }
    private void UpdateDirection()
    {
        Vector3 localScale = transform.localScale;
        if (transform.localScale.x > 0)
        {
            if (rb.linearVelocity.x < 0)
            {
                transform.localScale = new Vector3(-1 * localScale.x, localScale.y, localScale.z);
            }
        }
        else
        {
            if (rb.linearVelocity.x > 0)
            {
                transform.localScale = new Vector3(-1 * localScale.x, localScale.y, localScale.z);
            }
        }
    }
}
