namespace Game.Enemies.States
{
    /// <summary>
    /// Interface for enemy behavior states using the State Pattern.
    /// Each state defines its entry, update, and exit logic independently.
    /// </summary>
    public interface IEnemyState
    {
        /// <summary>
        /// Called once when the state becomes active.
        /// </summary>
        /// <param name="enemy">The enemy entering this state.</param>
        void EnterState(BaseEnemy enemy);

        /// <summary>
        /// Called every frame while this state is active.
        /// </summary>
        /// <param name="enemy">The enemy currently in this state.</param>
        void UpdateState(BaseEnemy enemy);

        /// <summary>
        /// Called when the state is about to be replaced or removed.
        /// </summary>
        /// <param name="enemy">The enemy exiting this state.</param>
        void ExitState(BaseEnemy enemy);
    }
}