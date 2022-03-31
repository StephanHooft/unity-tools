using StephanHooft.Extensions;

namespace StephanHooft.StateMachines
{
    /// <summary>
    /// A state to be used in a <see cref="IStateMachine"/>.
    /// </summary>
    public abstract class State : IState
    {
        #region Properties

        /// <summary>
        /// The <see cref="State"/>'s name.
        /// </summary>
        public string Name => name;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #endregion
        #region Fields

        private readonly string name;
        private System.Func<string, IState> stateRegister;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #endregion
        #region Constructor

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name">
        /// 
        /// </param>
        public State(string name)
        {
            this.name = name.MustNotBeNull();
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #endregion
        #region IState Implementation

        /// <summary>
        /// Perform set-up behaviour for the <see cref="State"/> upon being entered.
        /// </summary>
        public abstract void EnterState();

        /// <summary>
        /// Perform clean-up behaviour for the <see cref="State"/> upon being exited.
        /// </summary>
        public abstract void ExitState();

        /// <summary>
        /// Retrieve a <see cref="IState"/> reference from the <see cref="State"/>'s set state register.
        /// </summary>
        /// <param name="stateName">
        /// The <see cref="string"/> name of the <see cref="IState"/> to retrieve.
        /// </param>
        /// <returns>
        /// A <see cref="IState"/>.
        /// </returns>
        /// <exception cref="System.InvalidOperationException">
        /// If no state register delegate has been set to the <see cref="State"/>.
        /// </exception>
        IState IState.RetrieveStateFromRegister(string stateName)
            => stateRegister != null ?
            stateRegister.Invoke(stateName)
            : throw
                new System.InvalidOperationException(NoStateRegisterSet);

        /// <summary>
        /// Register a delegate that the <see cref="IState"/> can use to obtain a reference to other
        /// <see cref="IState"/>s from the same <see cref="IStateMachine"/>.
        /// </summary>
        /// <param name="stateRegister">
        /// The delegate to set.
        /// </param>
        /// <exception cref="System.InvalidOperationException">
        /// If a state register delegate has already been set to the <see cref="State"/>.
        /// </exception>
        void IState.SetStateRegister(System.Func<string, IState> stateRegister)
        {
            if (this.stateRegister != null)
                throw
                    new System.InvalidOperationException(StateRegisterAlreadySet);
            this.stateRegister = stateRegister;
        }

        /// <summary>
        /// Perform the update behaviour for the <see cref="State"/>.
        /// </summary>
        /// <param name="deltaTime">
        /// The amount of time (in seconds) that has passed since the prior update.
        /// </param>
        /// <returns>
        /// A <see cref="IState"/> if a transition to said <see cref="IState"/> is required.
        /// <see cref="null"/> if no <see cref="IState"/> transition is required.
        /// </returns>
        public abstract IState UpdateState(float deltaTime);

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #endregion
        #region Methods

        /// <summary>
        /// A method that <see cref="State"/>-inheriting classes can use to obtain references to other
        /// <see cref="IState"/>s used by the same <see cref="IStateMachine"/>.
        /// </summary>
        /// <param name="stateName">
        /// The <see cref="string"/> name of the <see cref="IState"/> to obtain.
        /// </param>
        /// <returns>
        /// A <see cref="IState"/>.
        /// </returns>
        protected IState GetState(string stateName)
            => ((IState)this).RetrieveStateFromRegister(stateName);

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #endregion
        #region Error Messages

        private string NoStateRegisterSet
            => string.Format("No state register has been set to the State.");

        private string StateRegisterAlreadySet
            => string.Format("A state register has already been set to the State.");

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #endregion
    }
}
