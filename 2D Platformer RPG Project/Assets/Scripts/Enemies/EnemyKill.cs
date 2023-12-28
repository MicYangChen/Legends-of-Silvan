using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyKill : MonoBehaviour
{
    ExpSystem expSystem;

    public GameObject[] itemDrops;
    public float[] dropChances;
    public int experience;

    public void Awake()
    {
        expSystem = GameObject.Find("Player").GetComponent<ExpSystem>();
    }

    public void KillEnemy()
    {
        expSystem.IncreaseExp(experience);
        Destroy(gameObject);
    }

    public void ItemDrop()
    {
        for (int i = 0; i < itemDrops.Length; i++)
        {
            if (Random.value <= dropChances[i])
            {
                Instantiate(itemDrops[i], transform.position, Quaternion.identity);
            }
        }
    }
}
