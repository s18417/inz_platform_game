namespace enemy_ranged
{
    public class rangedDyingState : rangedBaseState
    {
        public override void EnterState(rangedStateManager ranged)
        {
            ranged.anim.Play("ded");
        }

        public override void UpdateState(rangedStateManager ranged)
        {
        }
    }
}