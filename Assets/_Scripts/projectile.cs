using UnityEngine;

public class projectile : MonoBehaviour
{
    public int Arrowdamage = 5;
    public Vector2 moveSpeed = new Vector2(3, 0);
    public Vector2 knockback = new Vector2(0, 0);
    Rigidbody2D rb;
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Destroy(gameObject, 3f);
        rb.linearVelocity = new Vector2(moveSpeed.x * transform.localScale.x, moveSpeed.y);
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.layer == LayerMask.NameToLayer("ground"))
        {
            Destroy(gameObject);
            return;
        }
        Damageable damageable = col.GetComponent<Damageable>();
        if (damageable != null)
        {
            Vector2 deliveredKnockback = transform.localScale.x > 0 ? knockback : new Vector2(-knockback.x, knockback.y);
            bool gotHit = damageable.Hit(Arrowdamage, deliveredKnockback);
            if (gotHit)
                Debug.Log(col.name + "hit for" + Arrowdamage);
            Destroy(gameObject);
        }
    }
}
