using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackScript : MonoBehaviour
{
    private Animator anim;
    int _attackcounter = 1;
    float timer=0f;
    public float _maxDelay;
    public Transform attackPoint;
    public LayerMask enemyLayer;
    public float attackRange;
    

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (Input.GetButtonDown("Fire1")&&timer>0.25f)
        {
            _attackcounter++;
            if (_attackcounter > 3) _attackcounter = 0;
            anim.SetInteger("attackcounter",_attackcounter);
            timer = 0.0f;
        }

        if (timer > _maxDelay)
        {
            _attackcounter = 0;
            anim.SetInteger("attackcounter",_attackcounter);

        }
    }

    public void Attack(int dmg)
    {
        Collider2D[] hit = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayer);
      
        foreach (Collider2D enemy in hit)
        {
            IDamagable damagable = enemy.GetComponent<HealthSystem>();
            Debug.Log(damagable);
            if (damagable != null)
            {
                damagable.TakeDamage(dmg);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(attackPoint.position,attackRange);
    }
}
