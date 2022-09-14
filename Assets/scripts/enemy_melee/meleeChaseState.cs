
using UnityEditor.Media;
using UnityEngine;

public class meleeChaseState : meleeBaseState
{

    public override void EnterState(meleeStateManager melee)
    {
        Debug.Log("chase");
    }

    public override void UpdateState(meleeStateManager melee)
    {
        var distance = Vector3.Distance(melee.player.transform.position, melee.tr.position);
        if (melee._playerDetector.PlayerDetected)
        {
            if (distance < 7f)
            {
                var xd = melee.player.transform.position.x - melee.tr.position.x;
                if (xd > 0) melee.dir = 1;
                else melee.dir = -1;
            }

            if (melee.meleeColisionScript.canMoveForward && !melee.meleeColisionScript.isAgainstTheWall)
            { 
                melee.MoveForward();
            }
            else
            {
                melee.rb.velocity = new Vector2(0, 0);
                melee.anim.Play("HeavyBandit_CombatIdle");
            }

        }
         
        if (distance > 7f)
        {
            melee.SwitchState(melee.PatrolState);
        }

        if (distance < 2f)
        {
            melee.SwitchState(melee.AttackState);
        }
    }
}