using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyKill : MonoBehaviour
{
    public GameObject[] itemDrops;
    public float[] dropChances;

    public void KillEnemy()
    {
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
