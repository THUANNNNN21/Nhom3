using UnityEngine;

public class healthPickup : MonoBehaviour
{
    public int healthRestore = 10;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        Damageable damageable = col.GetComponent<Damageable>();
        if (damageable)
        {
            bool wasHealed = damageable.heal(healthRestore);
            if (wasHealed)
                Destroy(gameObject);
        }
    }
}
