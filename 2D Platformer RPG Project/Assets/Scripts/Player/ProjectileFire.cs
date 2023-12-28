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
            return Mathf.RoundToInt(playerStats.strength * playerStats.ranged);
        }
        set
        {
            playerStats.strength = value;
        }
    }

    public void FireProjectile()
    {
        GameObject projectile = Instantiate(projectilePrefab, launchPoint.position, projectilePrefab.transform.rotation);

        float angle = transform.eulerAngles.y;
        Vector3 firingDirection = new Vector3(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad), 0);
        
        // Vector3 originalScale = projectile.transform.localScale;

        projectile.transform.localScale = new Vector3(firingDirection.x * transform.localScale.x > 0 ? 1 : -1,
            projectile.transform.localScale.y, projectile.transform.localScale.z);
    }
}
