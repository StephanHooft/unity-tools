namespace StephanHooft.StateMachines
{
    /// <summary>
    /// A state to be used in a <see cref="IStateMachine{TEnum}"/>.
    /// </summary>
    /// <typeparam name="TEnum">
    /// A <see cref="System.Enum"/> that acts as a key for the <see cref="IState{TEnum}"/>s
    /// registered to the <see cref="StateMachine{TEnum}"/> upon instantiation.
    /// </typeparam>
    public abstract class State<TEnum> : IState<TEnum> where TEnum : System.Enum
    {
        #region Properties

        /// <summary>
        /// The <see cref="State{TEnum}"/>'s <typeparamref name="TEnum"/> key.
        /// </summary>
        TEnum IState<TEnum>.Key => key;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #endregion
        #region Fields

        private readonly TEnum key;
        private System.Func<TEnum, IState<TEnum>> stateRegister;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #endregion
        #region Constructor

        /// <summary>
        /// Creates a new <see cref="State{TEnum}"/>.
        /// </summary>
        /// <param name="key">
        /// The <typeparamref name="TEnum"/> key to assign to the <see cref="State{TEnum}"/>.
        /// </param>
        public State(TEnum key)
        {
            this.key = key;
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #endregion
        #region IState<TEnum> Implementation

        /// <summary>
        /// Performs set-up behaviour for the <see cref="State{TEnum}"/> upon being entered.
        /// </summary>
        public abstract void EnterState();

        /// <summary>
        /// Performs clean-up behaviour for the <see cref="State{IState}"/> upon being exited.
        /// </summary>
        public abstract void ExitState();

        /// <summary>
        /// Retrieves a <see cref="IState{TEnum}"/> reference from the <see cref="State{TEnum}"/>'s set state register.
        /// </summary>
        /// <param name="stateKey">
        /// The <typeparamref name="TEnum"/> key of the <see cref="IState"/> to retrieve.
        /// </param>
        /// <returns>
        /// A <see cref="IState{TEnum}"/>.
        /// </returns>
        /// <exception cref="System.InvalidOperationException">
        /// If no state register delegate has been set to the <see cref="State{TEnum}"/>.
        /// </exception>
        IState<TEnum> IState<TEnum>.RetrieveStateFromRegister(TEnum stateKey)
            => stateRegister != null ?
            stateRegister.Invoke(stateKey)
            : throw
                new System.InvalidOperationException(NoStateRegisterSet);

        /// <summary>
        /// Register a delegate that the <see cref="State{TEnum}"/> can use to obtain a reference to other
        /// <see cref="IState{TEnum}"/>s from the same <see cref="IStateMachine{TEnum}"/>.
        /// </summary>
        /// <param name="stateRegister">
        /// The delegate to set.
        /// </param>
        /// <exception cref="System.InvalidOperationException">
        /// If a state register delegate has already been set to the <see cref="State{TEnum}"/>.
        /// </exception>
        void IState<TEnum>.SetStateRegister(System.Func<TEnum, IState<TEnum>> stateRegister)
        {
            if (this.stateRegister != null)
                throw
                    new System.InvalidOperationException(StateRegisterAlreadySet);
            this.stateRegister = stateRegister;
        }

        /// <summary>
        /// Performs the update behaviour for the <see cref="State{TEnum}"/>.
        /// </summary>
        /// <param name="deltaTime">
        /// The amount of time (in seconds) that has passed since the prior update.
        /// </param>
        /// <returns>
        /// A <see cref="IState{TEnum}"/> if a transition to said <see cref="IState{TEnum}"/> is required.
        /// <see cref="null"/> if no <see cref="IState{TEnum}"/> transition is required.
        /// </returns>
        public abstract IState<TEnum> UpdateState(float deltaTime);

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #endregion
        #region Methods

        /// <summary>
        /// A method that <see cref="State{TEnum}"/>-inheriting classes can use to obtain references to other
        /// <see cref="IState{TEnum}"/>s used by the same <see cref="IStateMachine{TEnum}"/>.
        /// </summary>
        /// <param name="stateKey">
        /// The <typeparamref name="TEnum"/> key of the <see cref="IState"/> to obtain.
        /// </param>
        /// <returns>
        /// A <see cref="IState{TEnum"/>.
        /// </returns>
        protected IState<TEnum> GetState(TEnum stateKey)
            => ((IState<TEnum>)this).RetrieveStateFromRegister(stateKey);

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #endregion
        #region Error Messages

        private string NoStateRegisterSet
            => string.Format("No state register has been set to the State<{0}>.",
                typeof(TEnum).Name);

        private string StateRegisterAlreadySet
            => string.Format("A state register has already been set to the State<{0}>.",
                typeof(TEnum).Name);

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #endregion















    }
}