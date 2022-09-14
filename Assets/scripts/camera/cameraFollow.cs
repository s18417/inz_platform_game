using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class cameraFollow : MonoBehaviour
{
    private Transform player;

    public float smoothSpeed = 0.125f;
    public Vector3 offset;
    
    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
    }

    
    void LateUpdate()
    {
        Vector3 desiredPos = player.position + offset;
        Vector3 smoothedPos = Vector3.Lerp(transform.position, desiredPos, smoothSpeed);
        transform.position = smoothedPos;
    }
}
