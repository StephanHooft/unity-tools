namespace StephanHooft.StateMachines
{
    /// <summary>
    /// A finite <see cref="IState{TEnum}"/> machine.
    /// </summary>
    /// <typeparam name="TEnum">
    /// A <see cref="System.Enum"/> that acts as a key for the <see cref="IState{TEnum}"/>s
    /// registered to the <see cref="IStateMachine{TEnum}"/> upon instantiation.
    /// </typeparam>
    public interface IStateMachine<TEnum> where TEnum: System.Enum
    {
        #region Interface Events

        /// <summary>
        /// Invoke after the <see cref="IStateMachine{TEnum}"/> transitions to a different <see cref="IState{TEnum}"/>,
        /// or enters a <see cref="IState{TEnum}"/> for the first time after being instantiated.
        /// </summary>
        event System.Action<IStateMachine<TEnum>> OnStateChange;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #endregion
        #region Interface Properties

        /// <summary>
        /// The <typeparamref name="TEnum"/> key of the currently set <see cref="IState{TEnum}"/>.
        /// </summary>
        TEnum CurrentState { get; }

        /// <summary>
        /// <see cref="true"/> if the <see cref="IStateMachine{TEnum}"/> has an <see cref="IState{TEnum}"/> set.
        /// </summary>
        bool StateSet { get; }

        /// <summary>
        /// The amount of time (in seconds) that the current <see cref="IState{TEnum}"/> has been active.
        /// <para>This value resets to 0 every time a state transition occurs.</para>
        /// </summary>
        float TimeCurrentStateActive { get; }

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #endregion
        #region Interface Methods

        /// <summary>
        /// Order the <see cref="IStateMachine{TEnum}"/> to transition to a certain <see cref="IState{TEnum}"/>. 
        /// <para>The <see cref="IStateMachine{TEnum}"/> should call the
        /// <see cref="IState{TEnum}.ExitState"/> method of its current <see cref="IState{TEnum}"/> (if any) and the
        /// <see cref="IState{TEnum}.EnterState"/> method of the target <see cref="IState"/>.</para>
        /// </summary>
        /// <param name="targetState">
        /// The <typeparamref name="TEnum"/> key of the <see cref="IState{TEnum}"/> to set.
        /// </param>
        void SetState(TEnum targetState);

        /// <summary>
        /// Tell the current <see cref="IState{TEnum}"/> to call its <see cref="IState{TEnum}.UpdateState"/> member.
        /// Enact a state transition if required.
        /// </summary>
        /// <param name="deltaTime">
        /// The time difference (in seconds) since the previous update.
        /// </param>
        void UpdateCurrentState(float deltaTime);

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #endregion
    }
}
