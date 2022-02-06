using System.Collections.Generic;
using UnityEngine;

namespace StephanHooft.HybridUpdate
{
    /// <summary>
    /// A class that invokes a C# event whenever FixedUpdate() is called, and whenever more than one Update() is called
    /// after the latest FixedUpdate().
    /// <para>The resulting event can be used to update something once per Update(), without actually falling out of
    /// sync with FixedUpdate().</para>
    /// </summary>
    /// <remarks><para>When using the <see cref="HybridUpdateManager"/> to update a <see cref="MonoBehaviour"/>, you
    /// should replace the <see cref="MonoBehaviour"/>'s Update() and FixedUpdate() with a single "HybridUpdate" method.
    /// This method should then be registered with <see cref="RegisterUpdateCallback(System.Action{float}, int)"/> in
    /// OnEnable() and unregistered with <see cref="UnregisterUpdateCallback(System.Action{float})"/> in OnDisable().
    /// </para></remarks>
    public class HybridUpdateManager : MonoBehaviour
    {
        #region Properties

        private bool SkippedFixedUpdate => !fixedLast;
        private bool TimeStolen => totalStolenTime > 0f;
        private float HybridDeltaTime => TimeStolen ?
            Mathf.Max(Time.fixedDeltaTime - totalStolenTime, 0f) : Time.fixedDeltaTime;
        private static HybridUpdateManager LazyInstance
        {
            get
            {
                if (instance == null)
                {
                    instance = new GameObject("[HybridUpdater]").AddComponent<HybridUpdateManager>();
                    instance.transform.SetAsFirstSibling();
                }
                return
                    instance;
            }
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #endregion
        #region Fields

        private bool fixedLast;
        private float totalStolenTime = 0f;
        private readonly List<PriorityCallbackPair> callbacks = new List<PriorityCallbackPair>();
        private static HybridUpdateManager instance;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #endregion
        #region MonoBehaviour Implementation

        private void Awake()
        {
            if (instance != null)
            {
                Destroy(this);
                throw
                    new System.InvalidOperationException(
                        string.Format("Only one {0} may exist at once.", GetType().Name));
            }
        }

        private void FixedUpdate()
        {
            InvokeCallbacks(HybridDeltaTime);
            ClockFixedUpdate();
        }

        private void Update()
        {
            if (SkippedFixedUpdate)
                InvokeCallbacks(Time.deltaTime);
            ClockUpdate(Time.deltaTime);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #endregion
        #region Methods

        /// <summary>
        /// Register a method to callback whenever <see cref="HybridUpdateManager"/> fires.
        /// <para>The method must take one float argument, which serves as the "hybrid delta time"</para>
        /// that the method must use for any time-sensitive matters.
        /// </summary>
        /// <param name="callback">The method to register.</param>
        public static void RegisterUpdateCallback(System.Action<float> callback, int priority = 0)
        {
            if (callback == null)
                throw
                    new System.ArgumentNullException("callback");
            PriorityCallbackPair existing = LazyInstance.callbacks.Find(pair => pair.Callback == callback);
            if (existing != null)
                throw
                    new System.InvalidOperationException("Cannot register the same item twice.");
            LazyInstance.callbacks.Add(new PriorityCallbackPair(callback, priority));
            LazyInstance.callbacks.Sort();
        }

        /// <summary>
        /// Unregister a callback method that was previously registered to <see cref="HybridUpdateManager"/>.
        /// </summary>
        /// <param name="callback">The method to unregister.</param>
        public static void UnregisterUpdateCallback(System.Action<float> callback)
        {
            if (callback == null)
                throw
                    new System.ArgumentNullException("callback");
            if (instance == null)
                return;
            var existing = instance.callbacks.Find(pair => pair.Callback == callback);
            if (existing != null)
                instance.callbacks.Remove(existing);
        }

        private void InvokeCallbacks(float deltaTime)
        {
            foreach (PriorityCallbackPair pair in callbacks)
                pair.Callback.Invoke(deltaTime);
        }

        private void ClockFixedUpdate()
        {
            fixedLast = true;
            totalStolenTime = 0f;
        }

        private void ClockUpdate(float deltaTime)
        {
            if (!fixedLast)
                totalStolenTime += deltaTime;
            if (totalStolenTime > Time.fixedDeltaTime)
                totalStolenTime = Time.fixedDeltaTime;
            fixedLast = false;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #endregion
        #region PriorityCallbackPair Subclass

        private class PriorityCallbackPair : System.IComparable
        {
            #region Properties
            public System.Action<float> Callback { get; private set; }
            public int Priority { get; private set; }

            ////////////////////////////////////////////////////////////////////////////////////////////////////////////
            #endregion
            #region Constructors and Finaliser
            public PriorityCallbackPair(System.Action<float> callback, int priority)
            {
                Callback = callback;
                Priority = priority;
            }

            ~PriorityCallbackPair()
            { }

            ////////////////////////////////////////////////////////////////////////////////////////////////////////////
            #endregion
            #region Methods

            public int CompareTo(object obj)
            {
                if (obj is PriorityCallbackPair p)
                    return
                        Priority.CompareTo(p.Priority);
                else
                    throw
                        new System.NotImplementedException();
            }
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////
            #endregion
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #endregion
    }
}
