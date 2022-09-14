using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisionScript : MonoBehaviour
{
    private ColliderScript left_low_wall;
    private ColliderScript left_high_wall;
    private ColliderScript right_low_wall;
    private ColliderScript right_high_wall;
    private ColliderScript ground_collider;

   

    public bool isGrounded;
    public bool wallSensedBack;
    public bool wallSensedFront;
    
    void Start()
    {
        left_low_wall = transform.Find("wall_collider_left_low").GetComponent<ColliderScript>();
        left_high_wall = transform.Find("wall_collider_left_high").GetComponent<ColliderScript>();
        right_low_wall = transform.Find("wall_collider_right_low").GetComponent<ColliderScript>();
        right_high_wall = transform.Find("wall_collider_right_high").GetComponent<ColliderScript>();
        ground_collider = transform.Find("ground_collider").GetComponent<ColliderScript>();
    }

   
    void FixedUpdate()
    {
        if (left_high_wall.State() && left_low_wall.State()&& wallSensedBack == false)
        {
            wallSensedBack = true;
        }
        else if (!left_high_wall.State() && !left_low_wall.State() && wallSensedBack == true) wallSensedBack = false;

        if (right_high_wall.State() && right_low_wall.State()&& wallSensedFront == false)
        {
            wallSensedFront = true;
        }
        else if (!right_high_wall.State() && !right_low_wall.State() && wallSensedFront == true) wallSensedFront = false;

        if (ground_collider.State() &&isGrounded==false)
        {
            isGrounded = true;
           
        }
        else if (!ground_collider.State()&&isGrounded==true)
        {
            isGrounded = false;
           
        }
    }
    public void disableColliders(float time, string cols) {
        if (cols == "ground") ground_collider.disableCollider(time);
        else if (cols == "front")
        {
            right_high_wall.disableCollider(time);
            right_low_wall.disableCollider(time);
        }
        else if (cols=="back") {
            left_high_wall.disableCollider(time);
            left_low_wall.disableCollider(time);
        }
    }
}
