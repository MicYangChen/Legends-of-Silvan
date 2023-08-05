using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damageable : MonoBehaviour
{
    Animator animator;

    [SerializeField] private bool _isAlive = true;
    [SerializeField] private bool isInvincible = false;
    [SerializeField] private int _maxHealth = 100;
    [SerializeField] private int _health = 100;
    private float timeSinceHit = 0;
    public float invincibilityTime = 0.25f;

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

            if(_health < 0)
            {
                IsAlive = false;
            }
        }
    }

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if(isInvincible)
        {
            if(timeSinceHit > invincibilityTime)
            {
                isInvincible = false;
                timeSinceHit = 0;
            }

            timeSinceHit += Time.deltaTime;
        }
        Hit(10);
    }

    public void Hit(int damage)
    {
        if(IsAlive && !isInvincible)
        {
            Health -= damage;
            isInvincible = true;
        }
    }
}
