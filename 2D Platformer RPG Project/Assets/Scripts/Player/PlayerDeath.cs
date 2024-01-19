using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeath : MonoBehaviour
{
    Damageable damageable;
    public GameObject deathScreen;

    // Start is called before the first frame update
    void Start()
    {
        damageable = GetComponent<Damageable>();
        deathScreen = GameObject.Find("----------UI----------/GameCanvas/Death Screen");
    }

    // Update is called once per frame
    void Update()
    {
        if (damageable.Health <= 0)
        {
            deathScreen.SetActive(true);
        }
        else
        {
            deathScreen.SetActive(false);
        }
    }
}
