namespace StephanHooft.StateMachines
{
    /// <summary>
    /// A state to be used in a <see cref="IStateMachine{TEnum}"/>.
    /// </summary>
    /// <typeparam name="TEnum">
    /// A <see cref="System.Enum"/> that acts as a key for the <see cref="State{TEnum}"/>s
    /// registered to the <see cref="StateMachine{TEnum}"/> upon instantiation.
    /// </typeparam>
    public abstract class State<TEnum> : IState<TEnum> where TEnum : System.Enum
    {
        #region Properties

        /// <summary>
        /// The <see cref="State{TEnum}"/>'s <typeparamref name="TEnum"/> key.
        /// </summary>
        TEnum IState<TEnum>.Key => key;

        /// <summary>
        /// A delegate that the <see cref="State{TEnum}"/> can use to obtain a reference to other
        /// <see cref="IState{TEnum}"/>s from the same <see cref="IStateMachine{TEnum}"/>.
        /// </summary>
        System.Func<TEnum, IState<TEnum>> IState<TEnum>.StateRegister
        {
            get
                => stateRegister ?? throw
                    new System.InvalidOperationException(NoStateRegisterSet);
            set 
            {
                if (stateRegister != null)
                    throw
                        new System.InvalidOperationException(StateRegisterAlreadySet);
                stateRegister = value;
            }
        }
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
        #region Methods

        /// <summary>
        /// Obtains a reference to another <see cref="IState{TEnum}"/> used by the same
        /// <see cref="IStateMachine{TEnum}"/>.
        /// </summary>
        /// <param name="stateKey">
        /// The <typeparamref name="TEnum"/> key of the <see cref="IState{TEnum}"/> to obtain.
        /// </param>
        /// <returns>
        /// A <see cref="IState{TEnum}"/>.
        /// </returns>
        protected IState<TEnum> GetState(TEnum stateKey)
            => ((IState<TEnum>)this).StateRegister.Invoke(stateKey);

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #endregion
        #region Error Messages

        private string TEnumName
            => typeof(TEnum).Name;

        private string NoStateRegisterSet
            => string.Format("No state register has been set to the State<{0}>.", TEnumName);

        private string StateRegisterAlreadySet
            => string.Format("A state register has already been set to the State<{0}>.", TEnumName);

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #endregion
    }
}