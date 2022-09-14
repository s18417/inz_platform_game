using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    public float speed = 20f;
    public Rigidbody2D rb;
    void Start()
    {
        rb.velocity = transform.right * speed;
    }

    private void OnTriggerEnter2D(Collider2D enemy)
    {
        Destroy(gameObject);
        IDamagable damagable = enemy.GetComponent<HealthSystem>();
        Debug.Log(damagable);
        if (damagable != null)
        {
            damagable.TakeDamage(10);
        }
    }
}
