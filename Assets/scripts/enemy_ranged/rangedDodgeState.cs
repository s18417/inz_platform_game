using UnityEngine;

namespace enemy_ranged
{
    public class rangedDodgeState : rangedBaseState
    {
        private float dodgeCooldown = 1f;
        private bool usedDodge;
        private float stateTimer;
        public override void EnterState(rangedStateManager ranged)
        {
            Debug.Log("dodge");
            usedDodge = false;
            stateTimer = 0f;
        }

        public override void UpdateState(rangedStateManager ranged)
        {
            if (!ranged._wallSensor.PlayerDetected&& usedDodge ==false)
            {
                usedDodge = true;
                ranged.rb.AddForce(new Vector2(ranged.jumpForce*-ranged.dir *4, ranged.jumpForce*1.5f), 
                    ForceMode2D.Impulse);
                Debug.Log("USINGDODGE");
                ranged.anim.SetBool("idle",false);
                ranged.anim.SetTrigger("jump");
            }

            if (usedDodge)
            {
                stateTimer += Time.deltaTime;
            }

            if (stateTimer > 1f) 
            {
                if (ranged._shootSensor.PlayerDetected || ranged._dodgeSensor.PlayerDetected)
                    ranged.SwitchState(ranged.ShootState);
                else ranged.SwitchState(ranged.IdleState);
            }

            // if (ranged.rb.velocity.y > 0.1)
            // {
            //     Debug.Log("JUMP");
            //     
            //     ranged.anim.Play("jump");
            // } else ranged.anim.Play("idle");

        }
    }
}