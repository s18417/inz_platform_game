using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class meleeColisionScript : MonoBehaviour
{
    public ColliderScript groundSense;
    public ColliderScript wallSense;
    
    public bool canMoveForward;
    public bool isAgainstTheWall;
    
    void Start()
    {
        groundSense = transform.Find("lineOfSightGround").GetComponent<ColliderScript>();
        wallSense = transform.Find("wallSensor").GetComponent<ColliderScript>();
    }

    private void FixedUpdate()
    {
        if (groundSense.collided != null)
        {
            if (groundSense.collided.CompareTag("Ground")) 
                canMoveForward = true;
        }
        else
        {
            canMoveForward = false;
        }

        
        
       

        if (wallSense.collided != null )
        {
            if (wallSense.collided.CompareTag("Ground"))
                isAgainstTheWall = true;
        }else isAgainstTheWall = false;
        
        
    }
}
