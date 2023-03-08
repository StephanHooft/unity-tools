using StephanHooft.Attributes;
using StephanHooft.Collections;
using StephanHooft.Extensions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StephanHooft.HybridUpdate
{
    /// <summary>
    /// A <see cref="ScriptableObject"/> that allows <see cref="Behaviour"/>-inheriting objects to update
    /// whenever FixedUpdate is called, and also whenever more than one Update is called after the last FixedUpdate.
    /// <para>This enables a <see cref="Behaviour"/> to update at least once per frame update, while still
    /// remaining in sync with every call to FixedUpdate.</para>
    /// </summary>
    [CreateAssetMenu(fileName = "New HybridUpdater", menuName = "HybridUpdate/Hybrid Updater", order = 1)]
    public class HybridUpdater : ScriptableObject, IHybridUpdater, IEnumerable<(string, int)>
    {
        #region Properties

        /// <summary>
        /// Whether the <see cref="HybridUpdater"/> is paused or not.
        /// </summary>
        public bool Paused
            => paused;
        
        /// <summary>
        /// The <see cref="HybridUpdater"/>'s time speed. 1.0f is its default value.
        /// </summary>
        public float TimeSpeed
            => timeSpeed;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #endregion
        #region Fields

        private readonly Clock clock = new();
        private readonly CallbackCounter counter = new();
        private readonly SortKeyDictionary<System.Type, int, List<HybridUpdateCallback?>> callbackSets = new();

        [SerializeField]
        private bool paused = false;

        [SerializeField]
        private float timeSpeed = 1.0f;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #endregion
        #region IHybridUpdateClock Implementation

        void IHybridUpdater.ReportFixedUpdateCall()
        {
            if (counter.FixedUpdate())
            {
                InvokeCallbacks(clock.HybridDeltaTime);
                clock.ClockFixedUpdate();
            }
        }

        void IHybridUpdater.ReportUpdateCall(float deltaTime)
        {
            if (counter.Update())
            {
                if (clock.SkippedFixedUpdate)
                    InvokeCallbacks(deltaTime);
                clock.ClockUpdate(deltaTime);
            }
        }

        HybridUpdateCallback IHybridUpdater.Register
            (System.Type type, int priority, System.Action<float> callback)
        {
            if (callbackSets.IsEmpty())
            {
                clock.Reset();
                counter.Reset();
            }
            var list = GetOrAddSet(type, priority);
            var newCallback = new HybridUpdateCallback(callback, type);
            list.Add(newCallback);
            counter.Add();
            return
                newCallback;
        }

        void IHybridUpdater.Unregister(HybridUpdateCallback? callback)
        {
            var type = callback?.MustNotBeNull("callback").type;
            var set = callbackSets[type];
            set.Remove(callback);
            counter.Subtract();
            if (set.IsEmpty())
                callbackSets.Remove(callback?.type);
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #endregion
        #region IEnumerable Implementation

        public IEnumerator<(string, int)> GetEnumerator()
        {
            var enumerator = new List<(string, int)>();
            foreach(var key in callbackSets.Keys)
                enumerator.Add((key.FullName, callbackSets.SortKeyOf(key)));
            enumerator.Sort(CompareNameRankPairs);
            return
                enumerator.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
            => GetEnumerator();

        private static int CompareNameRankPairs((string, int rank) x, (string, int rank) y)
        {
            return
                x.rank.CompareTo(y.rank);
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #endregion
        #region Methods

        /// <summary>
        /// Can be used to pause the <see cref="HybridUpdater"/>, "freezing" all objects that are relying on it.
        /// </summary>
        /// <param name="pause">
        /// Whether or not the <see cref="HybridUpdater"/> should pause.
        /// </param>
        public void Pause(bool pause)
        {
            paused = pause;
        }

        /// <summary>
        /// Set the rate at which the <see cref="HybridUpdater"/>'s time advances.
        /// </summary>
        /// <param name="timeSpeed">
        /// The time speed to set. 1.0f is the default speed value.
        /// </param>
        public void SetTimeSpeed(float timeSpeed)
        {
            this.timeSpeed = timeSpeed;
        }

        private void InvokeCallbacks(float deltaTime)
        {
            if (!paused)
            {
                foreach (var list in callbackSets)
                    foreach (var callback in list)
                        callback?.action.Invoke(deltaTime * timeSpeed);
            }
        }

        private List<HybridUpdateCallback?> GetOrAddSet(System.Type type, int priority)
        {
            if (!callbackSets.ContainsKey(type))
            {
                var set = new List<HybridUpdateCallback?>();
                callbackSets.Add(type, priority, set);
                return
                    set;
            }
            else
                return
                    callbackSets[type];
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #endregion
        #region Behaviour Class

        /// <summary>
        /// A modified <see cref="MonoBehaviour"/>. Inherit from this class to make use of a 
        /// <see cref="HybridUpdater"/> for updates.
        /// </summary>
        public abstract class Behaviour : MonoBehaviour
        {
            #region Properties

            /// <summary>
            /// The relative priority of the class type. This determines the order in which objects of this type get
            /// updated compared to other objects. The returned value must be unique to the class.
            /// </summary>
            abstract protected int UpdatePriority { get; }

            ////////////////////////////////////////////////////////////////////////////////////////////////////////////
            #endregion
            #region Fields

            [SerializeField, HideWhilePlaying]
            private HybridUpdater updater;

            private IHybridUpdater iUpdater;
            private HybridUpdateCallback? callback;

            ////////////////////////////////////////////////////////////////////////////////////////////////////////////
            #endregion
            #region MonoBehaviour Implementation

            /// <summary>
            /// When overriding OnEnable(), be sure to call base.OnEnable() as well.
            /// </summary>
            protected virtual void OnEnable()
            {
                if (updater != null)
                {
                    iUpdater = updater;
                    callback = iUpdater.Register(GetType(), UpdatePriority, HybridUpdate);
                }
                else
                {
                    Debug.LogWarning(string.Format("A {0} cannot update without setting a {1} reference."
                        , typeof(Behaviour).Name, typeof(HybridUpdater).Name));
                }
            }

            /// <summary>
            /// When overriding OnDisable(), be sure to call base.OnDisable() as well.
            /// </summary>
            protected virtual void OnDisable()
            {
                if (callback != null)
                    iUpdater.Unregister(callback);
            }

            private void FixedUpdate()
            {
                if (callback != null)
                    iUpdater.ReportFixedUpdateCall();
            }

            private void Update()
            {
                if (callback != null)
                    iUpdater.ReportUpdateCall(Time.deltaTime);
            }

            ////////////////////////////////////////////////////////////////////////////////////////////////////////////
            #endregion
            #region Methods

            /// <summary>
            /// The update method that the <see cref="HybridUpdater"/> assigned to the
            /// <see cref="Behaviour"/> will call to update it. Replaces both Update() and FixedUpdate().
            /// </summary>
            /// <param name="deltaTime">The time difference since the last <see cref="HybridUpdate(float)"/> call.
            /// </param>
            protected abstract void HybridUpdate(float deltaTime);

            ////////////////////////////////////////////////////////////////////////////////////////////////////////////
            #endregion
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #endregion
        #region CallbackCounter Class

        private enum HybridUpdatePhase { None, FixedUpdate, Update }

        private class CallbackCounter
        {
            #region Properties

            public bool Counting => phase != HybridUpdatePhase.None;

            ////////////////////////////////////////////////////////////////////////////////////////////////////////////
            #endregion
            #region Fields

            private int callers;
            private int callersProcessed;
            private HybridUpdatePhase phase;

            ////////////////////////////////////////////////////////////////////////////////////////////////////////////
            #endregion
            #region Methods

            public void Add()
            {
                callers++;
                if (Counting)
                    callersProcessed++;
            }

            public bool FixedUpdate()
            {
                switch (phase)
                {
                    case HybridUpdatePhase.FixedUpdate:
                        callersProcessed++;
                        if (callersProcessed == callers)
                            phase = HybridUpdatePhase.None;
                        return
                            false;
                    case HybridUpdatePhase.None:
                        if (callers > 1)
                        {
                            phase = HybridUpdatePhase.FixedUpdate;
                            callersProcessed = 1;
                        }
                        return
                            true;
                    default:
                        throw
                            new System.NotImplementedException();
                };
            }

            public void Reset()
            {
                callers = 0;
                callersProcessed = 0;
                phase = HybridUpdatePhase.None;
            }

            public void Subtract()
            {
                callers--;
                if (Counting)
                    callersProcessed--;
            }

            public bool Update()
            {
                switch (phase)
                {
                    case HybridUpdatePhase.Update:
                        callersProcessed++;
                        if (callersProcessed == callers)
                            phase = HybridUpdatePhase.None;
                        return
                            false;
                    case HybridUpdatePhase.None:
                        if (callers > 1)
                        {
                            phase = HybridUpdatePhase.Update;
                            callersProcessed = 1;
                        }
                        return
                            true;
                    default:
                        throw
                            new System.NotImplementedException();
                };
            }
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////
            #endregion
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #endregion
        #region Clock Class

        private class Clock
        {
            #region Properties

            public bool SkippedFixedUpdate => !fixedLast;

            public float HybridDeltaTime => TimeStolen ?
                Mathf.Max(Time.fixedDeltaTime - totalStolenTime, 0f) : Time.fixedDeltaTime;

            private bool TimeStolen => totalStolenTime > 0f;

            ////////////////////////////////////////////////////////////////////////////////////////////////////////////
            #endregion
            #region Fields

            private bool fixedLast;
            private float totalStolenTime;

            ////////////////////////////////////////////////////////////////////////////////////////////////////////////
            #endregion
            #region Methods

            public void ClockFixedUpdate()
            {
                fixedLast = true;
                totalStolenTime = 0f;
            }

            public void ClockUpdate(float deltaTime)
            {
                if (!fixedLast)
                    totalStolenTime += deltaTime;
                if (totalStolenTime > Time.fixedDeltaTime)
                    totalStolenTime = Time.fixedDeltaTime;
                fixedLast = false;
            }

            public void Reset()
            {
                fixedLast = false;
                totalStolenTime = 0f;
            }
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////
            #endregion
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #endregion
    }
}