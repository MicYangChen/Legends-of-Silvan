using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Damageable : MonoBehaviour
{
    Animator animator;

    [SerializeField] private bool _isAlive = true;
    [SerializeField] private bool isInvincible = false;
    [SerializeField] private int _maxHealth = 100;
    [SerializeField] private int _health = 100;
    private float timeSinceHit = 0;
    public float invincibilityTime = 1f;

    private bool IsAlive
    {
        get
        {
            return _isAlive;
        }
        set
        {
            _isAlive = value;
            animator.SetBool(AnimationStrings.isAlive, value);
            Debug.Log("IsAlive set " +  value);
        }
    }

    // Velocity should not be changed while LockVelocity is true, but needs to be respected by other physics components like PlayerController
    public bool LockVelocity
    {
        get
        {
            return animator.GetBool(AnimationStrings.lockVelocity);
        }
        set
        {
            animator.SetBool(AnimationStrings.lockVelocity, value);
        }
    }

    public int MaxHealth
    {
        get
        {
            return _maxHealth;
        }
        set
        {
            _maxHealth = value;
        }
    }

    public int Health
    {
        get
        {
            return _health;
        }
        set
        {
            _health = value;

            if(_health <= 0)
            {
                IsAlive = false;
            }
        }
    }

    public UnityEvent<int, Vector2> damageableHit;

    public bool Hit(int damage, Vector2 knockback)
    {
        if(IsAlive && !isInvincible)
        {
            Health -= damage;
            isInvincible = true;

            // Notify other components that the damageable was hit to handle the knockback etc.
            animator.SetTrigger(AnimationStrings.hitTrigger);
            LockVelocity = true;
            damageableHit?.Invoke(damage, knockback);
            CharacterEvents.characterDamaged.Invoke(gameObject, damage);

            return true;
        }

        return false;
    }

    public void Heal(int healthRestore)
    {
        if(IsAlive)
        {
            int maxHealthRestore = Mathf.Max(MaxHealth - Health, 0);
            int successfulHealthRestore = Mathf.Min(maxHealthRestore, healthRestore);
            Health += successfulHealthRestore;
            CharacterEvents.characterHealed(gameObject, successfulHealthRestore);
        }
    }

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (isInvincible)
        {
            if (timeSinceHit > invincibilityTime)
            {
                isInvincible = false;
                timeSinceHit = 0;
            }

            timeSinceHit += Time.deltaTime;
        }
    }
}
