using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderScript : MonoBehaviour
{
    public Collider2D collided=null;
    public bool state = false;
    public float disableTimer = 0f;
    public bool ignoreGround = false;
    public bool State() {
        if (disableTimer > 0)
        {
            return false;
        }
        else return state;
    }

    public void disableCollider(float time) {
        disableTimer = time;
        
    }
    private void Update()
    {
        if (disableTimer > 0) {
            disableTimer -= Time.deltaTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        state = true;
        collided = collision;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        state = false;
        collided = null;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        collided = collision;
    }
}
