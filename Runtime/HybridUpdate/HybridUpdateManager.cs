using System;
using System.Collections.Generic;
using UnityEngine;

namespace StephanHooft.HybridUpdate
{
    /// <summary>
    /// A class that invokes a C# event whenever FixedUpdate() is called, and whenever more than one Update() is called after the latest FixedUpdate().
    /// <para>
    /// The resulting event can be used to update something once per Update(), without actually falling out of sync with FixedUpdate().
    /// </para>
    /// </summary>
    /// <remarks>When using the <see cref="HybridUpdateManager"/> to update a <see cref="MonoBehaviour"/>, you should replace the
    /// <see cref="MonoBehaviour"/>'s Update() and FixedUpdate() with a single "HybridUpdate" method. This method should then be registered with 
    /// <see cref="RegisterUpdateCallback(Action{float}, int)"/> in OnEnable() and unregistered with <see cref="UnregisterUpdateCallback(Action{float})"/>
    /// in OnDisable().</remarks>
    public class HybridUpdateManager : MonoBehaviour
    {
        private bool SkippedFixedUpdate => !fixedLast;
        private bool TimeStolen => totalStolenTime > 0f;
        private float HybridDeltaTime => TimeStolen ? Mathf.Max(Time.fixedDeltaTime - totalStolenTime, 0f) : Time.fixedDeltaTime;
        private static HybridUpdateManager LazyInstance
        {
            get
            {
                if(instance == null)
                    instance = new GameObject("[HybridUpdater]").AddComponent<HybridUpdateManager>();
                return instance;
            }
        }
        private bool fixedLast;
        private float totalStolenTime = 0f;
        private readonly List<PriorityCallbackPair> callbacks = new List<PriorityCallbackPair>();
        private static HybridUpdateManager instance;

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////

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

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// Register a method to callback whenever <see cref="HybridUpdateManager"/> fires.
        /// <para>The method must take one float argument, which serves as the "hybrid delta time"</para>
        /// that the method must use for any time-sensitive matters.
        /// </summary>
        /// <param name="callback">The method to register.</param>
        public static void RegisterUpdateCallback(Action<float> callback, int priority = 0)
        {
            if (callback == null) 
                throw new ArgumentNullException("callback");
            PriorityCallbackPair existing = LazyInstance.callbacks.Find(pair => pair.Callback == callback);
            if (existing != null) 
                throw new InvalidOperationException("Cannot add the same item twice.");
            LazyInstance.callbacks.Add(new PriorityCallbackPair(callback, priority));
            LazyInstance.callbacks.Sort();
        }

        /// <summary>
        /// Unregister a callback method that was previously registered to <see cref="HybridUpdateManager"/>.
        /// </summary>
        /// <param name="callback">The method to unregister.</param>
        public static void UnregisterUpdateCallback(Action<float> callback)
        {
            if (callback == null) 
                throw new ArgumentNullException("callback");
            if (instance == null) 
                return;
            var existing = instance.callbacks.Find(pair => pair.Callback == callback);
            if (existing != null)
                instance.callbacks.Remove(existing);
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////

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

        private class PriorityCallbackPair : IComparable
        {
            public Action<float> Callback { get; private set; }
            public int Priority { get; private set; }

            public PriorityCallbackPair(Action<float> callback, int priority)
            {
                Callback = callback;
                Priority = priority;
            }

            public int CompareTo(object obj)
            {
                if (obj is PriorityCallbackPair p)
                    return Priority.CompareTo(p.Priority);
                else 
                    throw new NotImplementedException();
            }
        }
    }
}
