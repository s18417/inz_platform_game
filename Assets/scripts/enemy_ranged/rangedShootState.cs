using UnityEngine;

namespace enemy_ranged
{
    public class rangedShootState : rangedBaseState
    {
        private float shootTime = 1f;
        private float timer = 0f;
        public override void EnterState(rangedStateManager ranged)
        {
            Debug.Log("shoot");
        }

        public override void UpdateState(rangedStateManager ranged)
        {
            if (ranged._shootSensor.PlayerDetected||ranged._dodgeSensor.PlayerDetected) timer += Time.deltaTime;
            if (timer > shootTime)
            {
                ranged.anim.SetBool("idle",false);
                ranged.anim.SetTrigger("shoot");
            } else ranged.anim.SetBool("idle",true);
            if (!ranged._shootSensor.PlayerDetected&&!ranged._dodgeSensor.PlayerDetected) ranged.SwitchState(ranged.IdleState);
        }
    }
}