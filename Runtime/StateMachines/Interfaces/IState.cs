namespace StephanHooft.StateMachines
{
    /// <summary>
    /// An interface for states to be used in a <see cref="IStateMachine"/>.
    /// </summary>
    public interface IState
    {
        #region Interface Properties

        /// <summary>
        /// The <see cref="IState"/>'s name.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// A delegate that the <see cref="IState"/> can use to obtain a reference to other <see cref="IState"/>s from
        /// the same <see cref="IStateMachine"/>.
        /// </summary>
        System.Func<string, IState> StateRegister { get; set; }

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #endregion
        #region Interface Methods

        /// <summary>
        /// Perform set-up behaviour for the <see cref="IState"/> upon being entered.
        /// </summary>
        /// <param name="deltaTime">
        /// The amount of time (in seconds) that has passed since the prior update.
        /// </param>
        void Enter(float deltaTime) { }

        /// <summary>
        /// Perform clean-up behaviour for the <see cref="IState"/> upon being exited.
        /// </summary>
        void Exit() { }

        /// <summary>
        /// Perform the update behaviour for the <see cref="IState"/>.
        /// </summary>
        /// <param name="deltaTime">
        /// The amount of time (in seconds) that has passed since the prior update.
        /// </param>
        /// <returns>
        /// A <see cref="IState"/> if a transition to said <see cref="IState"/> is required.
        /// <see cref="null"/> if no <see cref="IState"/> transition is required.
        /// </returns>
        IState Update(float deltaTime)
            => null;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #endregion
    }
}

