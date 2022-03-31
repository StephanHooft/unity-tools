namespace StephanHooft.StateMachines
{
    /// <summary>
    /// An interface for states to be used in a <see cref="IStateMachine{TEnum}"/>.
    /// </summary>
    /// <typeparam name="TEnum">
    /// A <see cref="System.Enum"/> that acts as a key for the <see cref="IState{TEnum}"/>s
    /// registered to the <see cref="IStateMachine{TEnum}"/> upon instantiation.
    /// </typeparam>
    public interface IState<TEnum> where TEnum: System.Enum
    {
        #region Interface Properties

        /// <summary>
        /// The <see cref="IState{TEnum}"/>'s <typeparamref name="TEnum"/> key.
        /// </summary>
        TEnum Key { get; }

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #endregion
        #region Interface Methods

        /// <summary>
        /// Perform set-up behaviour for the <see cref="IState{TEnum}"/> upon being entered.
        /// </summary>
        void EnterState();

        /// <summary>
        /// Perform clean-up behaviour for the <see cref="IState{IState}"/> upon being exited.
        /// </summary>
        void ExitState();

        /// <summary>
        /// Retrieve a <see cref="IState{TEnum}"/> reference from the <see cref="IState{TEnum}"/>'s set state register.
        /// </summary>
        /// <param name="state">
        /// The <typeparamref name="TEnum"/> key of the <see cref="IState"/> to retrieve.
        /// </param>
        /// <returns>
        /// A <see cref="IState{TEnum"/>.
        /// </returns>
        IState<TEnum> RetrieveStateFromRegister(TEnum state);

        /// <summary>
        /// Register a delegate that the <see cref="IState{TEnum}"/> can use to obtain a reference to other
        /// <see cref="IState{TEnum}"/>s from the same <see cref="IStateMachine{TEnum}"/>.
        /// </summary>
        /// <param name="stateRegister">
        /// The delegate to set.
        /// </param>
        void SetStateRegister(System.Func<TEnum, IState<TEnum>> stateRegister);

        /// <summary>
        /// Perform the update behaviour for the <see cref="IState{TEnum}"/>.
        /// </summary>
        /// <param name="deltaTime">
        /// The amount of time (in seconds) that has passed since the prior update.
        /// </param>
        /// <returns>
        /// A <see cref="IState{TEnum}"/> if a transition to said <see cref="IState{TEnum}"/> is required.
        /// <see cref="null"/> if no <see cref="IState{TEnum}"/> transition is required.
        /// </returns>
        IState<TEnum> UpdateState(float deltaTime);

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #endregion
    }
}
