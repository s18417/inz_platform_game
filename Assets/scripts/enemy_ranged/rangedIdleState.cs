using UnityEngine;

namespace enemy_ranged
{
    public class rangedIdleState : rangedBaseState
    {
        private float timer = 0f;
        private float turnaroundTimer = 5f;
        public override void EnterState(rangedStateManager ranged)
        {
            ranged.anim.SetBool("idle",true);   
            Debug.Log("idle");

        }

        public override void UpdateState(rangedStateManager ranged)
        {
            timer += Time.deltaTime;
            if (timer>turnaroundTimer)
            {
                ranged.Rotate();
                timer = 0f;
            }
            if (ranged._shootSensor.PlayerDetected) ranged.SwitchState(ranged.ShootState);
            if (ranged._dodgeSensor.PlayerDetected) ranged.SwitchState(ranged.DodgeState);
        }
    }
}