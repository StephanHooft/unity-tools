using StephanHooft.Extensions;
using System.Collections.Generic;

namespace StephanHooft.StateMachines
{
    /// <summary>
    /// A finite <see cref="IState{TEnum}"/> machine.
    /// </summary>
    /// <typeparam name="TEnum">
    /// A <see cref="System.Enum"/> that acts as a key for the <see cref="IState{TEnum}"/>s
    /// registered to the <see cref="StateMachine{TEnum}"/> upon instantiation.
    /// </typeparam>
    public sealed class StateMachine<TEnum> : IStateMachine<TEnum> where TEnum : System.Enum
    {
        #region Events

        /// <summary>
        /// Invoked after the <see cref="StateMachine{TEnum}"/> transitions to a different <see cref="IState{TEnum}"/>,
        /// or enters a <see cref="IState{TEnum}"/> for the first time after being instantiated.
        /// </summary>
        public event System.Action<IStateMachine<TEnum>> OnStateChange;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #endregion
        #region Properties

        /// <summary>
        /// The <typeparamref name="TEnum"/> key of the currently set <see cref="IState{TEnum}"/>.
        /// </summary>
        /// <exception cref="System.InvalidOperationException">
        /// If no <see cref="IState{TEnum}"/> is set.
        /// </exception>
        public TEnum CurrentState
            => StateSet
            ? currentState.Key
            : throw
                new System.InvalidOperationException(NoStateSet);

        /// <summary>
        /// <see cref="true"/> if the <see cref="StateMachine{TEnum}"/> has an <see cref="IState{TEnum}"/> set.
        /// </summary>
        public bool StateSet => currentState != null;

        /// <summary>
        /// The amount of time (in seconds) that the current <see cref="IState{TEnum}"/> has been active.
        /// <para>This value resets to 0 every time a state transition occurs.</para>
        /// </summary>
        /// <exception cref="System.InvalidOperationException">
        /// If no <see cref="IState{TEnum}"/> is set.
        /// </exception>
        public float TimeCurrentStateActive
            => StateSet
            ? timeCurrentStateActive
            : throw
                new System.InvalidOperationException(NoStateSet);

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #endregion
        #region Fields

        private IState<TEnum> currentState;
        private readonly Dictionary<TEnum, IState<TEnum>> states = new();
        private float timeCurrentStateActive;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #endregion
        #region Constructor

        /// <summary>
        /// Creates a new <see cref="StateMachine{TEnum}"/>.
        /// </summary>
        /// <param name="states">A collection of the <see cref="IState{TEnum}"/>s to be available to the
        /// <see cref="StateMachine{TEnum}"/>.</param>
        /// <exception cref="System.ArgumentException">
        /// If <paramref name="states"/> is empty.
        /// </exception>
        /// <exception cref="System.ArgumentNullException">
        /// If <paramref name="states"/> or any of its <see cref="IState{TEnum}"/>s are <see cref="null"/>.
        /// </exception>
        /// <exception cref="Exceptions.StateDuplicationException">
        /// If one of the <see cref="IState{TEnum}"/>s in <paramref name="states"/> is a duplicate.
        /// </exception>
        public StateMachine(IEnumerable<IState<TEnum>> states)
        {
            if (states == null)
                throw
                    new System.ArgumentNullException("states");
            var types = new List<System.Type>();
            foreach (var state in states)
            {
                if (state == null)
                    throw
                        new System.ArgumentNullException("states", NoNullPermittedInStateCollection);
                var key = state.Key;
                var type = state.GetType();
                if (this.states.ContainsKey(key))
                    throw
                        new Exceptions.StateDuplicationException(key);
                if (types.Contains(type))
                    throw
                        new Exceptions.StateDuplicationException(type);
                this.states.Add(state.Key, state);
                types.Add(type);
            }
            if (this.states.IsEmpty())
                throw
                    new System.ArgumentException(CollectionIsEmpty, "states");
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #endregion
        #region IStateMachine<TEnum> Implementation

        /// <summary>
        /// Orders the <see cref="StateMachine{TEnum}"/> to wnter a certain <see cref="IState{TEnum}"/>. 
        /// <para>The <see cref="StateMachine{TEnum}"/> will automatically call the
        /// <see cref="IState{TEnum}.Exit"/> method of its current <see cref="IState{TEnum}"/> (if any) and the
        /// <see cref="IState{TEnum}.Enter"/> method of the target <see cref="IState{TEnum}"/>.</para>
        /// </summary>
        /// <param name="targetState">
        /// The <typeparamref name="TEnum"/> key of the <see cref="IState{TEnum}"/> to set.
        /// </param>
        /// <exception cref="Exceptions.StateUnavailableException">
        /// If no <see cref="IState{TEnum}"/> with the set <typeparamref name="TEnum"/> key is registered to the
        /// <see cref="StateMachine{TEnum}"/>.
        /// </exception>
        public void Enter(TEnum targetState)
        {
            IState<TEnum> state;
            try
            {
                state = EnumToState(targetState);
            }
            catch (Exceptions.StateUnavailableException e)
            {
                throw
                    e;
            }
            SetState(state);
        }

        /// <summary>
        /// Tells the current <see cref="IState{TEnum}"/> to call its <see cref="IState{TEnum}.Update"/> member.
        /// This may cause a state transition.
        /// <para><see cref="Enter(TEnum)"/> must be called at least once before calling this method.</para>
        /// </summary>
        /// <param name="deltaTime">The time difference (in seconds) since the previous update.</param>
        /// <exception cref="System.InvalidOperationException">
        /// If no <see cref="IState{TEnum}"/> has been set.
        /// </exception>
        /// <exception cref="Exceptions.StateUnavailableException">
        /// If the current <see cref="IState{TEnum}"/> returns a <typeparamref name="TEnum"/> key for which no
        /// <see cref="IState{TEnum}"/> was registered to the <see cref="StateMachine{TEnum}"/>.
        /// </exception>
        public void Update(float deltaTime)
        {
            if (currentState is not null)
            {
                timeCurrentStateActive += deltaTime;
                var nextState = currentState.Update(deltaTime);
                if (!nextState.Equals(default(TEnum)))
                {
                    var state = EnumToState(nextState);
                    SetState(state, deltaTime);
                }
                return;
            }
            throw
                new System.InvalidOperationException(NoStateSet);
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #endregion
        #region Methods

        private void SetState(IState<TEnum> targetState, float deltaTime = 0f)
        {
            if (targetState != currentState)
            {
                if (currentState != null)
                {
                    currentState.Exit();
                    OnStateChange?.Invoke(this);
                }
                targetState.Enter(deltaTime);
                currentState = targetState;
                timeCurrentStateActive = 0f;
            }
        }

        private IState<TEnum> EnumToState(TEnum state)
        {
            if (!states.ContainsKey(state))
                throw
                    new Exceptions.StateUnavailableException(state);
            return
                states[state];
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #endregion
        #region Error Messages

        private string CollectionIsEmpty
            => string.Format("The provided IState<{0}> collection is empty.",
                typeof(TEnum).Name);

        private string NoNullPermittedInStateCollection
            => string.Format("The provided IState<{0}> collection contains null items.",
                typeof(TEnum).Name);

        private string NoStateSet
            => string.Format("No IState<{0}> is set to the StateMachine<{0}>.",
                typeof(TEnum).Name);

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #endregion
    }
}
