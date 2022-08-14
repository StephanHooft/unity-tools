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

        /// <summary>
        /// A delegate that the <see cref="State"/> can use to obtain a reference to other
        /// <see cref="IState}"/>s from the same <see cref="IStateMachine"/>.
        /// </summary>
        System.Func<string, IState> IState.StateRegister
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

        private readonly string name;
        private System.Func<string, IState> stateRegister;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #endregion
        #region Constructor

        /// <summary>
        /// Creates a new <see cref="State"/>.
        /// </summary>
        /// <param name="name">
        /// The <see cref="string"/> name to assign to the <see cref="State"/>.
        /// </param>
        public State(string name)
        {
            this.name = name.MustNotBeNull();
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #endregion
        #region Methods

        /// <summary>
        /// Obtains a reference to another <see cref="IState"/> used by the same <see cref="IStateMachine"/>.
        /// </summary>
        /// <param name="stateName">
        /// The <see cref="string"/> name of the <see cref="IState"/> to obtain.
        /// </param>
        /// <returns>
        /// A <see cref="IState"/>.
        /// </returns>
        protected IState GetState(string stateName)
            => ((IState)this).StateRegister.Invoke(stateName);

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
