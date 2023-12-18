using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class HealthPickUp : MonoBehaviour
{
    public int healthRestore = 10;
    public Vector3 spinRotationSpeed = new Vector3(0, 180, 0);

    public AudioClip pickUpSound;
    public float volume = 1f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Damageable damageable = collision.GetComponent<Damageable>();

        if(damageable)
        {
            damageable.Heal(healthRestore);
            AudioSource.PlayClipAtPoint(pickUpSound, transform.position, volume);
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        transform.eulerAngles += spinRotationSpeed * Time.deltaTime;
    }
}
