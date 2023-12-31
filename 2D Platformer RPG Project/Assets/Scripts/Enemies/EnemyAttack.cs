using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    private PlayerStats playerStats;

    public int attackDamage = 10;
    private float randomMultiplier = 1f;
    public Vector2 knockback = Vector2.zero;
    private void Start()
    {
        playerStats = GameObject.Find("StatManager").GetComponent<PlayerStats>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // See if it can be hit
        Damageable damageable = collision.GetComponent<Damageable>();

        if (damageable != null)
        {
            Vector2 deliveredKnockback = transform.parent.localScale.x > 0 ? knockback : new Vector2(-knockback.x, knockback.y);

            randomMultiplier = Random.Range(0.8f, 1.2f);
            int damageDealt = Mathf.Max(0, Mathf.RoundToInt((attackDamage * randomMultiplier) - (0.8f * playerStats.defense)));

            // Hit the target
            bool gotHit = damageable.Hit(damageDealt, deliveredKnockback);
            if (gotHit)
            {
                Debug.Log(collision.name + " hit for " + damageDealt);
                CharacterEvents.characterDamaged.Invoke(gameObject, damageDealt);
            }
        }
    }
}
