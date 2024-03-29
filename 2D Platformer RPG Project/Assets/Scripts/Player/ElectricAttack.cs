using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricAttack : MonoBehaviour
{
    private PlayerStats playerStats;

    private int modifiedATT = 0;
    private float randomMultiplier = 1f;
    public Vector2 knockback = Vector2.zero;

    public float electricBonus = 4f;
    private bool isCritical;

    private void Start()
    {
        playerStats = GameObject.Find("StatManager").GetComponent<PlayerStats>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // See if it can be hit
        Damageable damageable = collision.GetComponent<Damageable>();
        EnemyStats enemyStats = collision.GetComponent<EnemyStats>();

        if (damageable != null)
        {
            Vector2 deliveredKnockback = transform.parent.localScale.x > 0 ? knockback : new Vector2(-knockback.x, knockback.y);

            randomMultiplier = Random.Range(0.8f, 1.2f);

            float critRoll = Random.value;
            isCritical = critRoll <= playerStats.critChance;

            int damageDealt = Mathf.RoundToInt(Mathf.RoundToInt((Mathf.Max(playerStats.strength, modifiedATT) * electricBonus) * randomMultiplier));

            if (isCritical)
            {
                damageDealt *= 2;
                Debug.Log("Critical Damage!");
            }

            damageDealt = enemyStats.ModifyDamage(damageDealt, EnemyStats.DamageType.Electric);

            // Hit the target
            bool gotHit = damageable.Hit(damageDealt, deliveredKnockback);
            if (gotHit)
            {
                Debug.Log(collision.name + " hit for " + damageDealt);

                // If dealt damage is critical, display a critical damage UI instead of the regular one.
                if (isCritical)
                {
                    CharacterEvents.characterCritDamaged.Invoke(collision.gameObject, damageDealt);
                }
                else
                {
                    CharacterEvents.characterDamaged.Invoke(gameObject, damageDealt);
                }
            }
            isCritical = false;
        }
    }

    public void ChangeAttackDamage(int amount)
    {
        modifiedATT = playerStats.strength + amount;
    }
}
