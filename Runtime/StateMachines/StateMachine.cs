using StephanHooft.Extensions;
using System.Collections.Generic;

namespace StephanHooft.StateMachines
{
    /// <summary>
    /// A finite <see cref="IState"/> machine.
    /// </summary>
    public sealed class StateMachine : IStateMachine
    {
        #region Events

        /// <summary>
        /// Invoked after the <see cref="StateMachine"/> transitions to a different <see cref="IState"/>, or enters a
        /// <see cref="IState"/> for the first time after being instantiated.
        /// </summary>
        public event System.Action<IStateMachine> OnStateChange;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #endregion
        #region Properties

        /// <summary>
        /// The name of the currently set <see cref="IState"/>.
        /// </summary>
        /// <exception cref="System.InvalidOperationException">
        /// If no <see cref="IState"/> is set.
        /// </exception>
        public string CurrentState
            => StateSet
            ? currentState.Name
            : throw
                new System.InvalidOperationException(NoStateSet);

        /// <summary>
        /// <see cref="true"/> if the <see cref="StateMachine"/> has an <see cref="IState"/> set.
        /// </summary>
        public bool StateSet => currentState != null;

        /// <summary>
        /// The amount of time (in seconds) that the current <see cref="IState"/> has been active.
        /// <para>This value resets to 0 every time a state transition occurs.</para>
        /// </summary>
        /// <exception cref="System.InvalidOperationException">
        /// If no <see cref="IState"/> is set.
        /// </exception>
        public float TimeCurrentStateActive
            => StateSet
                ? timeCurrentStateActive
                : throw
                    new System.InvalidOperationException(NoStateSet);

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #endregion
        #region Fields

        private IState currentState;
        private readonly List<IState> states = new();
        private float timeCurrentStateActive;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #endregion
        #region Constructor

        /// <summary>
        /// Creates a new <see cref="StateMachine"/>.
        /// </summary>
        /// <param name="states">A collection of the <see cref="IState"/>s to be available to the
        /// <see cref="StateMachine"/>.</param>
        /// <exception cref="System.ArgumentException">
        /// If <paramref name="states"/> is empty, or if one of its <see cref="IState"/>s is invalid.
        /// </exception>
        /// <exception cref="System.ArgumentNullException">
        /// If <paramref name="states"/> or any of its <see cref="IState"/>s are <see cref="null"/>.
        /// </exception>
        /// <exception cref="Exceptions.StateDuplicationException">
        /// If one of the <see cref="IState"/>s in <paramref name="states"/> is a duplicate.
        /// </exception>
        public StateMachine(IEnumerable<IState> states)
        {
            if (states == null)
                throw
                    new System.ArgumentNullException("states");
            var types = new List<System.Type>();
            var names = new List<string>();
            foreach (var state in states)
            {
                if (state == null)
                    throw
                        new System.ArgumentNullException(NoNullPermittedInStateCollection);
                var type = state.GetType();
                if (types.Contains(type))
                    throw
                        new Exceptions.StateDuplicationException(type);
                var name = state.Name;
                if (name.IsNullOrEmpty())
                    throw
                        new System.ArgumentException(StateNameInvalid(type));
                if (names.Contains(name))
                    throw
                        new System.ArgumentException(StateNameDuplicate(name, type));
                this.states.Add(state);
                state.StateRegister = NameToState;
                types.Add(type);
                names.Add(name);
            }
            if (this.states.IsEmpty())
                throw
                    new System.ArgumentException(CollectionIsEmpty, "states");
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #endregion
        #region IStateMachine Implementation

        /// <summary>
        /// Orders the <see cref="StateMachine"/> to enter a certain <see cref="IState"/>. 
        /// <para>The <see cref="StateMachine"/> will automatically call the <see cref="IState.Exit"/> method of
        /// its current <see cref="IState"/> (if any) and the <see cref="IState.Enter"/> method of the target
        /// <see cref="IState"/>.</para>
        /// </summary>
        /// <param name="targetStateName">
        /// The <see cref="string"/> name of the <see cref="IState"/> to set.
        /// </param>
        /// <exception cref="Exceptions.StateUnavailableException">
        /// If no <see cref="IState"/> with the set <see cref="string"/> name is registered to the
        /// <see cref="StateMachine"/>.
        /// </exception>
        public void Enter(string targetStateName)
        {
            IState state;
            try
            {
                state = NameToState(targetStateName);
            }
            catch (Exceptions.StateUnavailableException e)
            {
                throw
                    e;
            }
            SetState(state);
        }

        /// <summary>
        /// Tells the current <see cref="IState"/> to call its <see cref="IState.Update"/> member.
        /// This may cause a state transition.
        /// </summary>
        /// <param name="deltaTime">The time difference (in seconds) since the previous update.</param>
        /// <exception cref="System.InvalidOperationException">
        /// If no <see cref="IState"/> is set.
        /// </exception>
        public void Update(float deltaTime)
        {
            if (currentState == null)
                throw
                    new System.InvalidOperationException(NoStateSet);
            timeCurrentStateActive += deltaTime;
            var nextState = currentState.Update(deltaTime);
            if (nextState != null)
                SetState(nextState, deltaTime);
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #endregion
        #region Methods

        private IState NameToState(string name)
        {
            var foundState = states.Find(state => state.Name == name);
            if (foundState == null)
                throw
                    new Exceptions.StateUnavailableException(name);
            return
                foundState;
        }

        private void SetState(IState targetState, float deltaTime = 0f)
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
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #endregion
        #region Error Messages

        private string CollectionIsEmpty
            => string.Format("The provided IState collection is empty.");

        private string NoNullPermittedInStateCollection
            => string.Format("The provided IState collection contains null items.");

        private string NoStateSet
            => string.Format("No IState is set to the StateMachine.");

        private string StateNameDuplicate(string name, System.Type type)
            => string.Format(
                "The name '{0}' of IState with type '{1}' has already been registered to the StateMachine.",
                name, type.Name);

        private string StateNameInvalid(System.Type type)
            => string.Format("The name of IState '{0}' may not be null or empty.",
                type.Name);

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #endregion
    }
}
