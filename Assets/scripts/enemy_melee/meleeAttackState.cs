using System.Collections;
using UnityEngine;

public class meleeAttackState : meleeBaseState
{
    private meleeStateManager meleeMan;
    private float attackSpeed = 3f;
    private float timeSinceAttack = 0f;
    private float windUp = 1f;
    public override void EnterState(meleeStateManager melee)
    {
        Debug.Log("attack");
        meleeMan = melee;
        windUp = 1f;
        melee.anim.Play("HeavyBandit_CombatIdle");
        melee.rb.velocity = new Vector2(0, 0);
    }

    public override void UpdateState(meleeStateManager melee)
    {
        timeSinceAttack += Time.deltaTime;
        
        
        var distance = Vector3.Distance(melee.player.transform.position, melee.tr.position);
        if (distance < 2f)
        {
            
            var xd = melee.player.transform.position.x - melee.tr.position.x;
            if (xd > 0) melee.dir = 1;
            else melee.dir = -1;

            windUp -= Time.deltaTime;
            if (windUp <= 0&& timeSinceAttack>attackSpeed)
            {
                melee.anim.Play("HeavyBandit_Attack");
                timeSinceAttack = 0f;
            }
            
               if (timeSinceAttack>1.5f) melee.anim.Play("HeavyBandit_CombatIdle");
            
            
        }

        if (distance > 2f && distance < 7f)
        {
            melee.SwitchState(melee.ChaseState);
        }

        if (distance > 7f)
        {
            melee.SwitchState(melee.PatrolState);
        }
    }
   


}
