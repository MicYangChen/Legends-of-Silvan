using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ParallaxEffect : MonoBehaviour
{
    public Camera cam;
    public Transform followTarget;

    // Start position for the parallax object
    Vector2 startingPosition;

    // Start Z  value of the parallax object
    float startingZ;

    Vector2 camMoveSinceStart => (Vector2)cam.transform.position - startingPosition;

    // The further the object from player, the faster the ParallaxEffect object will move. Z value closer to the target = moves slower
    float parallaxFactor => Mathf.Abs(distanceFromTargetz) / clippingPlane;

    float distanceFromTargetz => transform.position.z - followTarget.transform.position.z;

    // If object is in front of the target, use nearClipPlane. If behind the target,  use farClipPlane
    float clippingPlane => (cam.transform.position.z + (distanceFromTargetz > 0 ? cam.farClipPlane : cam.nearClipPlane));

    // Start is called before the first frame update
    void Start()
    {
        startingPosition = transform.position;
        startingZ = transform.position.z;

        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player != null)
        {
            followTarget = player.transform;
        }
        else
        {
            Debug.LogError("Player gameObject not found in this Scene");
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Target moves -> move the parallax object the same distance times a multiplier
        Vector2 newPosition = startingPosition + camMoveSinceStart * parallaxFactor;

        // X & Y position changes based on target travel speed times the parallax factor. Z stays consistent
        transform.position = new Vector3(newPosition.x, newPosition.y, startingZ);
    }
}
