using UnityEngine;

public class attack : MonoBehaviour
{
    public Vector2 knockback = Vector2.zero;
    public int attackDamage = 10;
    private void OnTriggerEnter2D(Collider2D other)
    {
        Damageable damageable = other.GetComponent<Damageable>();
        if (damageable != null)
        {
            Vector2 deliveredKnockback = transform.parent.localScale.x > 0 ? knockback : new Vector2(-knockback.x, knockback.y);
            bool gotHit = damageable.Hit(attackDamage, deliveredKnockback);
            if (gotHit)
                Debug.Log(other.name + "hit for" + attackDamage);
        }
    }
}
