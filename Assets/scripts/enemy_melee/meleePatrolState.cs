
using UnityEngine;

public class meleePatrolState : meleeBaseState
{
    private float timeSinceTrun = 0;
    public override void EnterState(meleeStateManager melee)
    { 
        Vector2 movDir = new Vector2(-melee.dir, 0);
        melee.tr.right = movDir;
        Debug.Log("patrol");
    }

    public override void UpdateState(meleeStateManager melee)
    {
       
       
       
        melee.MoveForward();
        timeSinceTrun += Time.deltaTime;
        if (!melee.meleeColisionScript.canMoveForward || melee.meleeColisionScript.isAgainstTheWall)
        {
            if (timeSinceTrun > 0.3)
            {
                if (melee.dir == 1) melee.dir = -1;
                else melee.dir = 1;
                timeSinceTrun = 0;
            }
        }

        if (melee._playerDetector.PlayerDetected)
        {
            melee.SwitchState(melee.ChaseState);
        }
    }
}
