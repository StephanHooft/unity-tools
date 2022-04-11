namespace StephanHooft.StateMachines
{
    /// <summary>
    /// A finite <see cref="IState"/> machine.
    /// </summary>
    public interface IStateMachine
    {
        #region Interface Events

        /// <summary>
        /// Invoke after the <see cref="IStateMachine"/> transitions to a different <see cref="IState"/>, or enters a
        /// <see cref="IState"/> for the first time after being instantiated.
        /// </summary>
        event System.Action<IStateMachine> OnStateChange;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #endregion
        #region Interface Properties

        /// <summary>
        /// The name of the currently set <see cref="IState"/>.
        /// </summary>
        string CurrentState { get; }

        /// <summary>
        /// <see cref="true"/> if the <see cref="IStateMachine"/> has an <see cref="IState"/> set.
        /// </summary>
        bool StateSet { get; }

        /// <summary>
        /// The amount of time (in seconds) that the current <see cref="IState"/> has been active.
        /// <para>This value resets to 0 every time a state transition occurs.</para>
        /// </summary>
        float TimeCurrentStateActive { get; }

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #endregion
        #region Interface Methods

        /// <summary>
        /// Order the <see cref="IStateMachine"/> to enter a certain <see cref="IState"/>. 
        /// <para>The <see cref="IStateMachine"/> should call the <see cref="IState.Exit"/> method of its current
        /// <see cref="IState"/> (if any) and the <see cref="IState.Enter"/> method of the target
        /// <see cref="IState"/>.</para>
        /// </summary>
        /// <param name="targetStateName">
        /// The <see cref="string"/> name of the <see cref="IState"/> to set.
        /// </param>
        void Enter(string targetStateName);

        /// <summary>
        /// Tell the current <see cref="IState"/> to call its <see cref="IState.Update"/> member.
        /// Enact a state transition if required.
        /// </summary>
        /// <param name="deltaTime">
        /// The time difference (in seconds) since the previous update.
        /// </param>
        void Update(float deltaTime);

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #endregion
    }
}
