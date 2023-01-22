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

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #endregion
        #region Fields

        private readonly TEnum key;

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
    }
}