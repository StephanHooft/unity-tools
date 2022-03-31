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

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #endregion
        #region Interface Methods

        /// <summary>
        /// Perform set-up behaviour for the <see cref="IState"/> upon being entered.
        /// </summary>
        void EnterState();

        /// <summary>
        /// Perform clean-up behaviour for the <see cref="IState"/> upon being exited.
        /// </summary>
        void ExitState();

        /// <summary>
        /// Retrieve a <see cref="IState"/> reference from the <see cref="IState"/>'s set state register.
        /// </summary>
        /// <param name="stateName">
        /// The <see cref="string"/> name of the <see cref="IState"/> to retrieve.
        /// </param>
        /// <returns>
        /// A <see cref="IState"/>.
        /// </returns>
        IState RetrieveStateFromRegister(string stateName);

        /// <summary>
        /// Register a delegate that the <see cref="IState"/> can use to obtain a reference to other
        /// <see cref="IState"/>s from the same <see cref="IStateMachine"/>.
        /// </summary>
        /// <param name="stateRegister">
        /// The delegate to set.
        /// </param>
        void SetStateRegister(System.Func<string, IState> stateRegister);

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
        IState UpdateState(float deltaTime);

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #endregion
    }
}

