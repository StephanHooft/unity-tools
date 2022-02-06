namespace StephanHooft.StateMachines
{
    /// <summary>
    /// A finite <see cref="IState"/> machine.
    /// </summary>
    public sealed class StateMachine
    {
        #region Events

        /// <summary>
        /// Invoked when the <see cref="StateMachine"/> sets an <see cref="IState"/> for the first time.
        /// </summary>
        public event System.Action<IState> OnStateMachineStart;

        /// <summary>
        /// Invoked when the <see cref="StateMachine"/> is about to switch to a different <see cref="IState"/>, but
        /// before the <see cref="IState.ExitState"/> method of the <see cref="CurrentState"/> is called.
        /// </summary>
        public event System.Action<IState, IState> OnStateChangeImpending;

        /// <summary>
        /// Invoked when the <see cref="StateMachine"/> is about to call the <see cref="IState.ExitState"/> method of
        /// its <see cref="CurrentState"/>.
        /// </summary>
        public event System.Action<IState> OnExitingState;

        /// <summary>
        /// Invoked when the <see cref="StateMachine"/> is about to call the <see cref="IState.EnterState"/> method of a
        /// new <see cref="IState"/> after exiting its prior <see cref="IState"/>.
        /// </summary>
        public event System.Action<IState> OnEnteringState;

        /// <summary>
        /// Invoked when the <see cref="StateMachine"/> has finished entering a new <see cref="IState"/>.
        /// </summary>
        public event System.Action<IState> OnStateChangeCompleted;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #endregion
        #region Properties

        /// <summary>
        /// The name assigned to the <see cref="StateMachine"/> upon instantiation.
        /// Cannot be changed after the <see cref="StateMachine"/> has been instantiated.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// The currently active <see cref="IState"/>.
        /// </summary>
        public IState CurrentState { get; private set; }

        /// <summary>
        /// True if the <see cref="StateMachine"/> has an <see cref="IState"/> set.
        /// </summary>
        public bool StateSet => CurrentState != null;

        /// <summary>
        /// The amount of time (in seconds) that the current <see cref="IState"/> has been active. This value resets to
        /// 0 every time a state transition occurs.
        /// </summary>
        public float TimeCurrentStateActive { get; private set; }

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #endregion
        #region Fields

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #endregion
        #region Constructors and Finaliser

        /// <summary>
        /// Create a new <see cref="StateMachine"/>.
        /// </summary>
        /// <param name="name">The <see cref="string"/> name to assign to the new <see cref="StateMachine"/>.</param>
        public StateMachine(string name)
        {
            Name = name;
        }

        /// <summary>
        /// Finaliser
        /// </summary>
        ~StateMachine()
        { }

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #endregion
        #region Methods

        /// <summary>
        /// Tell the <see cref="CurrentState"/> to call its <see cref="IState.UpdateState"/> member.
        /// This may cause a state transition.
        /// </summary>
        /// <param name="deltaTime">The time differential since the previous time that this method was called.</param>
        /// <returns>True if the <see cref="IState.UpdateState"/> was allowed to proceed without a state transition.
        /// False if a state transition has been called during this update.</returns>
        public void UpdateCurrentState(float deltaTime)
        {
            if (CurrentState == null)
                throw
                    new System.InvalidOperationException("StateMachine has no set state to update.");
            TimeCurrentStateActive += deltaTime;
            var nextState = CurrentState.UpdateState();
            if (nextState != null)
                SetState(nextState);
        }

        /// <summary>
        /// Order <see cref="StateMachine"/> to transition to a different <see cref="IState"/>. 
        /// <para>The <see cref="StateMachine"/> will automatically call the <see cref="IState.EnterState"/> method of
        /// its current <see cref="IState"/> and the <see cref="IState.ExitState"/> method of the target
        /// <see cref="IState"/>.</para>
        /// </summary>
        /// <param name="targetState">The <see cref="IState"/> to transition to.</param>
        public void SetState(IState targetState)
        {
            if (targetState == null)
                throw
                    new System.ArgumentNullException("targetState");
            if (targetState != CurrentState)
            {
                if (CurrentState == null)
                    OnStateMachineStart?.Invoke(targetState);
                else
                    OnStateChangeImpending?.Invoke(CurrentState, targetState);
                if (CurrentState != null)
                {
                    OnExitingState?.Invoke(CurrentState);
                    CurrentState.ExitState();
                    OnEnteringState?.Invoke(targetState);
                }
                targetState.EnterState();
                CurrentState = targetState;
                TimeCurrentStateActive = 0f;
                OnStateChangeCompleted?.Invoke(CurrentState);
            }
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #endregion
    }
}
