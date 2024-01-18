using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class FindPlayerCam : MonoBehaviour
{
    void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player != null)
        {
            CinemachineVirtualCamera virtualCamera = GetComponent<CinemachineVirtualCamera>();

            if (virtualCamera != null)
            {
                virtualCamera.Follow = player.transform;
            }
            else
            {
                Debug.LogError("CinemachineVirtualCamera component not found on this gameObject");
            }
        }
        else
        {
            Debug.LogError("Player gameObject not found in this Scene");
        }
    }
}
