namespace StephanHooft.StateMachines
{
    /// <summary>
    /// An interface for states to be used in a <see cref="StateMachine"/>.
    /// </summary>
    public interface IState
    {
        #region Interface Methods
        /// <summary>
        /// Perform set-up behaviour for the <see cref="IState"/> upon being entered.
        /// </summary>
        void EnterState();

        /// <summary>
        /// Perform the behaviour belonging to the active <see cref="IState"/>.
        /// </summary>
        /// <param name="deltaTime">The amount of time that has passed since the prior update.</param>
        /// <returns>A <see cref="IState"/> if a transition to said <see cref="IState"/> is required. Null if no <see cref="IState"/>
        /// transition is required.</returns>
        IState UpdateState(float deltaTime);

        /// <summary>
        /// Perform clean-up behaviour for the <see cref="IState"/> upon being exited.
        /// </summary>
        void ExitState();

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #endregion
    }
}

