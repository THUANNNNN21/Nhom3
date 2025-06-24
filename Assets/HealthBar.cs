using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Damageable damageable;
    public Image healthFill;
    void Update()
    {
        if (damageable != null && damageable.maxHealth > 0)
        {
            float percent = (float)damageable.Health / damageable.maxHealth;
            healthFill.fillAmount = Mathf.Clamp01(percent);
        }
    }
}
