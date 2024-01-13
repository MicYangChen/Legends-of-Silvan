using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastFire : MonoBehaviour
{
    private PlayerStats playerStats;
    public Transform launchPoint;
    public GameObject projectilePrefab;

    private void Start()
    {
        playerStats = GameObject.Find("StatManager").GetComponent<PlayerStats>();
    }

    public int FireMagicPower
    {
        get
        {
            return Mathf.RoundToInt(playerStats.strength * 2);
        }
        set
        {
            playerStats.strength = value;
        }
    }

    public void CastFireball()
    {
        GameObject projectile = Instantiate(projectilePrefab, launchPoint.position, projectilePrefab.transform.rotation);

        float angle = transform.eulerAngles.y;
        Vector3 firingDirection = new Vector3(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad), 0);

        // Vector3 originalScale = projectile.transform.localScale;

        projectile.transform.localScale = new Vector3(firingDirection.x * transform.localScale.x > 0 ? 1 : -1,
            projectile.transform.localScale.y, projectile.transform.localScale.z);
    }
}
