using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class meleeStateManager : MonoBehaviour
{
    
    [SerializeField]
    internal meleeColisionScript meleeColisionScript;

    [SerializeField] internal playerDetector _playerDetector;
    
    public Rigidbody2D rb;
    public Animator anim;
    public float dir;
    public Transform tr;
    public float speed;
    public GameObject player;
    public HealthSystem hp;
    
    public Transform attackPoint;
    public LayerMask enemyLayer;
    public float attackRange;
    
    meleeBaseState currentState;
    public meleeAttackState AttackState = new meleeAttackState();
    public meleeChaseState ChaseState = new meleeChaseState();
    public meleePatrolState PatrolState = new meleePatrolState();
    public meleeDyingState DyingState = new meleeDyingState();
    
   
    void Start()
    {
        player = GameObject.FindWithTag("Player");

        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        dir = 1;
        tr = transform;
        currentState = PatrolState;
        currentState.EnterState(this);
        hp = GetComponent<HealthSystem>();
    }

  
    void Update()
    {
        currentState.UpdateState(this);
        if (hp._health <= 0) SwitchState(DyingState);
    }

    public void SwitchState(meleeBaseState state)
    {
        currentState = state;
        state.EnterState(this);
    }

    public void MoveForward()
    {
        rb.velocity = new Vector2(dir * speed, rb.velocity.y);
        Vector2 movDir = new Vector2(-dir, 0);
        tr.right = movDir;
        anim.Play("HeavyBandit_Run");
    }

    public void Die()
    {
        Destroy(this.gameObject);
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(attackPoint.position,attackRange);
    
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

    

}
