using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollo : MonoBehaviour
{
    [SerializeField]
    private Transform player;
    [SerializeField]
    private Vector3 offset;

    void LateUpdate()
    {

        if (player != null)
        {
            Vector3 newPosition = player.position + offset;
            newPosition.z = transform.position.z;
            transform.position = newPosition;
        }
    }
}
