using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private ProjectileFire projectileFire;

    public Vector2 moveSpeed = new Vector2(3f, 0);
    public Vector2 knockback = new Vector2(0, 0);
    private float randomMultiplier = 1f;

    Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        projectileFire = GameObject.Find("Player").GetComponentInChildren<ProjectileFire>();
    }

    void Start()
    {
        rb.velocity = new Vector2(moveSpeed.x * transform.localScale.x, moveSpeed.y);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Damageable damageable = collision.GetComponent<Damageable>();

        if (damageable != null)
        {
            Vector2 deliveredKnockback = transform.localScale.x > 0 ? knockback : new Vector2(-knockback.x, knockback.y);
            
            randomMultiplier = Random.Range(0.75f, 1.25f);

            int damageDealt = Mathf.RoundToInt(projectileFire.AttackPower * randomMultiplier); // Bow deals R.ATT Damage

            bool gotHit = damageable.Hit(damageDealt, deliveredKnockback);
            if (gotHit)
            {
                Debug.Log(collision.name + " hit for " + damageDealt);
                Destroy(gameObject);
            }
        }
    }
}

