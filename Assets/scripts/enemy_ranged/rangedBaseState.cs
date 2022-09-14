namespace enemy_ranged
{
    public abstract class rangedBaseState
    {
        public abstract void EnterState(rangedStateManager ranged);
        public abstract void UpdateState(rangedStateManager ranged);
    }
}