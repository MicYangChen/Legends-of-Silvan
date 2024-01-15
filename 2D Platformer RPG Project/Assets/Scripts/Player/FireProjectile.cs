using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireProjectile : MonoBehaviour
{
    private CastFire castFire;
    private PlayerStats playerStats;

    public Vector2 moveSpeed = new Vector2(3f, 0);
    public Vector2 knockback = new Vector2(0, 0);
    private float randomMultiplier = 1f;
    public float maxTravelDistance = 20f;
    private Vector2 initialPosition;

    Rigidbody2D rb;
    BoxCollider2D boxCollider;
    Animator anim;

    private bool isCritical;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
        castFire = GameObject.Find("Player").GetComponentInChildren<CastFire>();
        playerStats = GameObject.Find("StatManager").GetComponent<PlayerStats>();
    }

    void Start()
    {
        rb.velocity = new Vector2(moveSpeed.x * transform.localScale.x, moveSpeed.y);
        initialPosition = transform.position;
    }

    void Update()
    {
        float currentDistance = Vector2.Distance(initialPosition, transform.position);

        if (currentDistance > maxTravelDistance)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Damageable damageable = collision.GetComponent<Damageable>();
        EnemyStats enemyStats = collision.GetComponent<EnemyStats>();

        if (damageable != null)
        {
            Vector2 deliveredKnockback = transform.localScale.x > 0 ? knockback : new Vector2(-knockback.x, knockback.y);

            randomMultiplier = Random.Range(0.75f, 1.25f);

            float critRoll = Random.value;
            isCritical = critRoll <= playerStats.critChance;

            int damageDealt = Mathf.RoundToInt(castFire.FireMagicPower * randomMultiplier); // Fire cast deals Fire Damage

            if (isCritical)
            {
                damageDealt *= 2;
                Debug.Log("Critical Damage!");
            }

            damageDealt = enemyStats.ModifyDamage(damageDealt, EnemyStats.DamageType.Fire); // Check for resistances

            bool gotHit = damageable.Hit(damageDealt, deliveredKnockback);
            if (gotHit)
            {
                Debug.Log(collision.name + " hit for " + damageDealt);

                if (isCritical)
                {
                    CharacterEvents.characterCritDamaged.Invoke(collision.gameObject, damageDealt);
                }
                else
                {
                    CharacterEvents.characterDamaged.Invoke(gameObject, damageDealt);
                }
                anim.SetTrigger("explode");
                StartCoroutine(WaitForAnimation());
            }
            isCritical = false;
        }
    }

    IEnumerator WaitForAnimation()
    {
        // Wait until the current state is not playing the "explode" animation
        while (anim.GetCurrentAnimatorStateInfo(0).IsName("explode"))
        {
            yield return null;
        }
        Destroy(gameObject);
    }

}
