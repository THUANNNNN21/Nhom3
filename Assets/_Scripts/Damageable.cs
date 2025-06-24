using UnityEngine;
using UnityEngine.Events;

public class Damageable : MonoBehaviour
{
    public bool LockVelocity
    {
        get { return animator.GetBool(AnimationString.lockVelocity); }
        set
        {
            animator.SetBool(AnimationString.lockVelocity, value);
        }
    }
    public UnityEvent<int, Vector2> damagebleHit;
    Animator animator;
    [SerializeField]
    private int _maxHealth = 100;
    public int maxHealth
    {
        get { return _maxHealth; }
        set { _maxHealth = value; }
    }
    public GameOverManager gameOverManager;
    [SerializeField]
    private int _Health = 100;
    public int Health
    {
        get { return _Health; }
        set
        {
            _Health = value;
            //nếu máu giảm xuống dưới 0, nhân vật ko còn sống
            if (_Health <= 0)
            {
                IsAlive = false;
                gameOverManager.ShowGameOver();
            }
        }
    }
    private bool _isAlive = true;
    public bool IsAlive
    {
        get { return _isAlive; }
        set
        {
            _isAlive = value;
            animator.SetBool(AnimationString.isAlive, value);
            Debug.Log("isAlive set" + value);
        }

    }

    void Awake()
    {
        animator = GetComponent<Animator>();
    }
    private bool isInvincible = false;
    [SerializeField]
    private float invincibleTime = 0.25f;
    private float Timer = 0;
    // Update is called once per frame
    void Update()
    {
        if (isInvincible)
        {
            if (Timer > invincibleTime)
            {
                isInvincible = false;
                Timer = 0;
            }
            Timer += Time.deltaTime;
        }
    }
    public bool Hit(int damage, Vector2 knockback)
    {
        if (IsAlive && !isInvincible)
        {
            Health -= damage;
            isInvincible = true;
            LockVelocity = true;
            animator.SetTrigger(AnimationString.hitTrigger);
            damagebleHit?.Invoke(damage, knockback);
            CharacterEvent.characterDamaged.Invoke(gameObject, damage);
            return true;
        }
        return false;
    }
    public bool heal(int healthRestore)
    {
        if (IsAlive && Health < maxHealth)
        {
            int maxheal = Mathf.Max(maxHealth - Health, 0);
            int actualHeal = Mathf.Min(maxheal, healthRestore);
            Health += actualHeal;
            CharacterEvent.characterHealed(gameObject, actualHeal);
            return true;
        }
        return false;
    }
}
