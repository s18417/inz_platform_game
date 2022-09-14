using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class meleeDyingState : meleeBaseState
{
    public override void EnterState(meleeStateManager melee)
    {
        melee.anim.Play("HeavyBandit_Recover");
    }

    public override void UpdateState(meleeStateManager melee)
    {
    }
}
