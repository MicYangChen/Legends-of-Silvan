using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickUp : MonoBehaviour
{
    public int healthRestore = 10;
    public Vector3 spinRotationSpeed = new Vector3(0, 180, 0);

    void Start()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Damageable damageable = collision.GetComponent<Damageable>();

        if(damageable)
        {
            damageable.Heal(healthRestore);
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        transform.eulerAngles += spinRotationSpeed * Time.deltaTime;
    }

}
