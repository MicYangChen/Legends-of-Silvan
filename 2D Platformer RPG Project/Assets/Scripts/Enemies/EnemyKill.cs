using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyKill : MonoBehaviour
{
    public GameObject[] itemDrops;

    public void KillEnemy()
    {
        Destroy(gameObject);
    }
}
