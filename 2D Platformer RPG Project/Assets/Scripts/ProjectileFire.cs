using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ProjectileFire : MonoBehaviour
{
    private PlayerStats playerStats;
    public Transform launchPoint;
    public GameObject projectilePrefab;

    private void Start()
    {
        playerStats = GameObject.Find("StatManager").GetComponent<PlayerStats>();
    }

    public int AttackPower
    {
        get
        {
            return playerStats.ranged;
        }
        set
        {
            playerStats.ranged = value;
        }
    }

    public void FireProjectile()
    {
        GameObject projectile = Instantiate(projectilePrefab, launchPoint.position, projectilePrefab.transform.rotation);
        Vector3 originalScale = projectile.transform.localScale;

        projectile.transform.localScale = new Vector3(originalScale.x * transform.localScale.x > 0 ? 1 : -1,
            originalScale.y, originalScale.z);
    }
}
