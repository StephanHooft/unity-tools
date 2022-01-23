using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace StephanHooft.ProgressIndicators
{
    /// <summary>
    /// Utility class to create and track <see cref="Indicator"/>s.
    /// </summary>
    public sealed class Progress
    {
        /// <summary>
        /// Returns the global average progression of all active <see cref="Indicator"/>s.
        /// </summary>
        public float GlobalProgress => CalculateGlobalProgress();

        /// <summary>
        /// Returns true if there is at least one running <see cref="Indicator"/>, false otherwise.
        /// </summary>
        public static bool Running => Instance.HasRunningIndicators();

        /// <summary>
        /// An event raised when a new <see cref="Indicator"/> starts.
        /// </summary>
        public static event Action<Indicator> Added
        {
            add => Instance.AddedInstance += value;
            remove => Instance.AddedInstance -= value;
        }

        /// <summary>
        /// An event raised when a <see cref="Indicator"/> is removed.
        /// </summary>
        public static event Action<Indicator> Removed
        {
            add => Instance.RemovedInstance += value;
            remove => Instance.RemovedInstance -= value;
        }

        /// <summary>
        /// An event raised when a <see cref="Indicator"/>'s state updates.
        /// </summary>
        public static event Action<Indicator> Updated
        {
            add => Instance.UpdatedInstance += value;
            remove => Instance.UpdatedInstance -= value;
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////

        private static Progress Instance => lazy.Value;
        private static readonly Lazy<Progress> lazy = new Lazy<Progress> (() => new Progress());
        private event Action<Indicator> AddedInstance;
        private event Action<Indicator> RemovedInstance;
        private event Action<Indicator> UpdatedInstance;
        private readonly Dictionary<int,Indicator> indicators = new Dictionary<int,Indicator>();
        private static string IndicatorClass => typeof(Indicator).ToString();

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// Constructor deliberately kept private. <see cref="Instance"/> will call this instructor when accessed for the first time.
        /// </summary>
        private Progress() 
        {}

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// Returns an enumerator to loop over all <see cref="Indicator"/>s.
        /// </summary>
        /// <returns>The enumerable <see cref="Indicator"/>s. </returns>
        public static IEnumerable<Indicator> EnumerateItems()
        {
            return
                Instance.indicators.Values;
        }

        /// <summary>
        /// Checks whether a <see cref="Indicator"/> with the specified ID exists.
        /// </summary>
        /// <param name="id">The <see cref="Indicator"/>'s unique ID.</param>
        /// <returns></returns>
        public static bool Exists(int id)
        {
            if (Instance.indicators.ContainsKey(id))
                return
                    true;
            return
                false;
        }

        /// <summary>
        /// Marks the <see cref="Indicator"/> as finished and invokes its finish calback.
        /// <para><see cref="Indicator"/>s without a parent are automatically removed upon being finished.</para>
        /// </summary>
        /// <param name="id">The <see cref="Indicator"/>'s unique ID.</param>
        public static void Finish(int id)
        {
            if (!Exists(id))
                throw
                    new ArgumentException("No Progress.Indicator with this ID value exists.", "id");
            Instance.indicators[id].Finish();
        }

        /// <summary>
        /// Gets the number of available <see cref="Indicator"/>s.
        /// </summary>
        /// <returns>The number of available <see cref="Indicator"/>s.</returns>
        public static int GetCount()
        {
            return
                Instance.indicators.Count;
        }

        /// <summary>
        /// Gets the current step for a <see cref="Indicator"/>.
        /// </summary>
        /// <param name="id">The <see cref="Indicator"/>'s unique ID.</param>
        /// <returns>The <see cref="Indicator"/>'s current step.</returns>
        public static int GetCurrentStep(int id)
        {
            if (!Exists(id))
                throw
                    new ArgumentException("No Progress.Indicator with this ID value exists.", "id");
            return
                Instance.indicators[id].CurrentStep;
        }

        /// <summary>
        /// Gets a <see cref="Indicator"/>'s name.
        /// </summary>
        /// <param name="id">The <see cref="Indicator"/>'s unique ID.</param>
        /// <returns>The matching <see cref="Indicator"/>'s name.</returns>
        public static string GetName(int id)
        {
            if (!Exists(id))
                throw
                    new ArgumentException("No Progress.Indicator with this ID value exists.", "id");
            return
                Instance.indicators[id].Name;
        }

        /// <summary>
        /// Get the unique ID of the <see cref="Indicator"/>'s parent, if any.
        /// </summary>
        /// <param name="id">The <see cref="Indicator"/>'s unique ID.</param>
        /// <returns>The unique ID of the <see cref="Indicator"/>'s parent. 
        /// If the <see cref="Indicator"/> is not a child of any other <see cref="Indicator"/>, returns -1.</returns>
        public static int GetParentId(int id)
        {
            if (!Exists(id))
                throw
                    new ArgumentException("No Progress.Indicator with this ID value exists.", "id");
            return
                Instance.indicators[id].ParentID;
        }

        /// <summary>
        /// Get the <see cref="Indicator"/>'s parent, if any.
        /// </summary>
        /// <param name="id">The <see cref="Indicator"/>'s unique ID.</param>
        /// <returns>The <see cref="Indicator"/>'s parent.</returns>
        public static Indicator GetParent(int id)
        {
            if (!Exists(id))
                throw
                    new ArgumentException("No Progress.Indicator with this ID value exists.", "id");
            return
                Instance.indicators[id].Parent;
        }

        /// <summary>
        /// Gets a <see cref="Indicator"/>'s progress.
        /// </summary>
        /// <param name="id">The <see cref="Indicator"/>'s unique ID.</param>
        /// <returns>The current progress.</returns>
        public static float GetProgress(int id)
        {
            if (!Exists(id))
                throw
                    new ArgumentException("No Progress.Indicator with this ID value exists.", "id");
            return
                Instance.indicators[id].Progress;
        }

        /// <summary>
        /// Gets a reference to a <see cref="Indicator"/>.
        /// </summary>
        /// <param name="id">The <see cref="Indicator"/>'s unique ID.</param>
        /// <returns>The <see cref="Indicator"/>, or null if no <see cref="Indicator"/>. with the provided <paramref name="id"/> exists.</returns>
        public static Indicator GetIndicatorById(int id)
        {
            if (!Exists(id))
                return
                    null;
            return
                Instance.indicators[id];
        }

        /// <summary>
        /// Returns the topmost <see cref="Indicator"/> in an <see cref="Indicator"/>'s hierarchy.
        /// </summary>
        /// <param name="id">The <see cref="Indicator"/>'s unique ID.</param>
        /// <returns>The topmost <see cref="Indicator"/> in the <see cref="Indicator"/>'s hierarchy.</returns>
        public static Indicator GetRoot(int id)
        {
            if (!Exists(id))
                throw
                    new ArgumentException("No Progress.Indicator with this ID value exists.", "id");
            return
                Instance.indicators[id].Root;
        }

        /// <summary>
        /// Gets the number of running <see cref="Indicator"/>s.
        /// </summary>
        /// <returns>The number of running <see cref="Indicator"/>s.</returns>
        public static int GetRunningProgressCount()
        {
            var count = 0;
            foreach (Indicator indicator in Instance.indicators.Values)
                if (indicator.Running) 
                    count++;
            return
                count;
        }

        /// <summary>
        /// Gets the timestamp of when a <see cref="Indicator"/> started.
        /// </summary>
        /// <param name="id">The <see cref="Indicator"/>'s unique ID.</param>
        /// <returns>The <see cref="Indicator"/>'s start timestamp.</returns>
        public static DateTime GetStartTime(int id)
        {
            if (!Exists(id))
                throw
                    new ArgumentException("No Progress.Indicator with this ID value exists.", "id");
            return
                Instance.indicators[id].StartTime;
        }

        /// <summary>
        /// Gets a <see cref="Indicator"/>'s current status.
        /// </summary>
        /// <param name="id">The <see cref="Indicator"/>'s unique ID.</param>
        /// <returns>The <see cref="Indicator"/>'s current status.</returns>
        public static IndicatorStatus GetStatus(int id)
        {
            if (!Exists(id))
                throw
                    new ArgumentException("No Progress.Indicator with this ID value exists.", "id");
            return
                Instance.indicators[id].Status;
        }

        /// <summary>
        /// Gets a <see cref="Indicator"/>'s step label.
        /// </summary>
        /// <param name="id">The <see cref="Indicator"/>'s unique ID.</param>
        /// <returns>The <see cref="Indicator"/>'s step label.</returns>
        public static string GetStepLabel(int id)
        {
            if (!Exists(id))
                throw
                    new ArgumentException("No Progress.Indicator with this ID value exists.", "id");
            return
                Instance.indicators[id].StepLabel;
        }

        /// <summary>
        /// Gets a <see cref="Indicator"/>'s total steps.
        /// </summary>
        /// <param name="id">The <see cref="Indicator"/>'s unique ID.</param>
        /// <returns>The <see cref="Indicator"/>'s total steps.</returns>
        public static int GetTotalSteps(int id)
        {
            if (!Exists(id))
                throw
                    new ArgumentException("No Progress.Indicator with this ID value exists.", "id");
            return
                Instance.indicators[id].TotalSteps;
        }

        /// <summary>
        /// Gets the last time that a <see cref="Indicator"/> last changed.
        /// </summary>
        /// <param name="id">The <see cref="Indicator"/>'s unique ID.</param>
        /// <returns>The timestamp of the <see cref="Indicator"/>'s last update.</returns>
        public static DateTime GetUpdateTime(int id)
        {
            if (!Exists(id))
                throw
                    new ArgumentException("No Progress.Indicator with this ID value exists.", "id");
            return
                Instance.indicators[id].UpdateTime;
        }

        /// <summary>
        /// Gets a <see cref="Indicator"/>'s weight value.
        /// </summary>
        /// <param name="id">The <see cref="Indicator"/>'s unique ID.</param>
        /// <returns>The <see cref="Indicator"/>'s weight value.</returns>
        public static int GetWeight(int id)
        {
            if (!Exists(id))
                throw
                    new ArgumentException("No Progress.Indicator with this ID value exists.", "id");
            return
                Instance.indicators[id].Weight;
        }

        /// <summary>
        /// Registers a callback that is called when a <see cref="Indicator"/> is finished.
        /// </summary>
        /// <param name="id">The <see cref="Indicator"/>'s unique ID.</param>
        /// <param name="callback">This method is called when the <see cref="Indicator"/> is finished.</param>
        public static void RegisterFinishCallback(int id, Action<Indicator> callback)
        {
            if (!Exists(id))
                throw
                    new ArgumentException("No Progress.Indicator with this ID value exists.", "id");
            if (callback == null)
                throw
                    new ArgumentNullException("callback");
            Instance.indicators[id].RegisterFinishCallback(callback);
        }

        /// <summary>
        /// Removes an active <see cref="Indicator"/>.
        /// </summary>
        /// <param name="id">The <see cref="Indicator"/>'s unique ID.</param>
        public static void Remove(int id)
        {
            if (!Exists(id))
                throw
                    new ArgumentException("No Progress.Indicator with this ID value exists.", "id");
            Remove(Instance.indicators[id]);
        }

        /// <summary>
        /// Reports a <see cref="Indicator"/>'s current status.
        /// </summary>
        /// <param name="id">The <see cref="Indicator"/>'s unique ID.</param>
        /// <param name="progress">A new progress value between 0 and 1.</param>
        public static void Report(int id, float progress)
        {
            if (!Exists(id))
                throw
                    new ArgumentException("No Progress.Indicator with this ID value exists.", "id");
            if (progress < 0f || progress > 1f)
                throw
                    new ArgumentOutOfRangeException("newProgress", "newProgress must be a value between 0f and 1f");
            Instance.indicators[id].Report(progress);
        }

        /// <summary>
        /// Reports a <see cref="Indicator"/>'s current status.
        /// </summary>
        /// <param name="id">The <see cref="Indicator"/>'s unique ID.</param>
        /// <param name="currentStep">An updated current step.</param>
        /// <param name="totalSteps">An updated total number of steps.</param>
        public static void Report(int id, int currentStep, int totalSteps)
        {
            if (!Exists(id))
                throw
                    new ArgumentException("No Progress.Indicator with this ID value exists.", "id");
            if (totalSteps < 1)
                throw
                    new ArgumentOutOfRangeException("newtotalSteps", "newTotalSteps must be 1 or greater.");
            if (currentStep > totalSteps)
                throw
                    new ArgumentOutOfRangeException("newCurrentTurnStep", "newCurrentTurnStep cannot be greater than newTotalSteps");
            Instance.indicators[id].Report(currentStep, totalSteps);
        }

        /// <summary>
        /// Sets the label that displays a <see cref="Indicator"/>'s steps.
        /// </summary>
        /// <param name="id">The <see cref="Indicator"/>'s unique ID.</param>
        /// <param name="stepLabel">The <see cref="Indicator"/>'s new step label.</param>
        public static void SetStepLabel(int id, string stepLabel)
        {
            if (!Exists(id))
                throw
                    new ArgumentException("No Progress.Indicator with this ID value exists.", "id");
            Instance.indicators[id].SetStepLabel(stepLabel);
        }

        /// <summary>
        /// Starts a new <see cref="Indicator"/>.
        /// </summary>
        /// <param name="name">The new <see cref="Indicator"/>'s name.</param>
        /// <param name="parentID">The unique ID of the parent <see cref="Indicator"/>, if any.
        /// If the <see cref="Indicator"/> should have no parent, pass -1.</param>
        /// <param name="weight">The weight of the new <see cref="Indicator"/>. Must be 1 or higher.</param>
        /// <returns>The new <see cref="Indicator"/>'s unique ID.</returns>
        public static int Start(string name = "", int parentID = -1, int weight = 1)
        {
            if (parentID >= 0 && !Exists(parentID))
                throw
                    new ArgumentException("No " + IndicatorClass + " with ID " + parentID + " exists.", "parentID");
            if (weight < 1)
                throw
                    new ArgumentOutOfRangeException("weight", "weight must be 1 or higher.");
            var id = 0;
            while (Instance.indicators.ContainsKey(id)) // create a unique identifier
                id++;
            var indicator = FormatterServices.GetUninitializedObject(typeof(Indicator)) as Indicator;
            IIndicator starter = indicator;
            starter.Start(id, name, weight);
            Instance.indicators.Add(id, indicator);
            Instance.AddedInstance?.Invoke(indicator);
            if(parentID >= 0)
                starter.SetParent(parentID);
            return
                id;
        }

        /// <summary>
        /// Starts a new <see cref="Indicator"/>.
        /// </summary>
        /// <param name="totalSteps">The total number of steps for the new <see cref="Indicator"/>. (Must be 1 or greater.)</param>
        /// <param name="name">The new <see cref="Indicator"/>'s name.</param>
        /// <param name="stepLabel">The label that displays the new <see cref="Indicator"/>'s steps.</param>
        /// <param name="parentID">The unique ID of the parent <see cref="Indicator"/>, if any. 
        /// If the <see cref="Indicator"/> should have no parent, pass -1.</param>
        /// <param name="weight">The weight of the new <see cref="Indicator"/>. Must be 1 or higher.</param>
        /// <returns>The new <see cref="Indicator"/>'s unique ID.</returns>
        public static int Start(int totalSteps, string name = "", string stepLabel = "", int parentID = -1, int weight = 1)
        {
            if (totalSteps < 1)
                throw
                    new ArgumentOutOfRangeException("totalSteps", "totalSteps must be 1 or greater.");
            if (parentID >= 0 && !Exists(parentID))
                throw
                    new ArgumentException("No " + IndicatorClass + " with ID " + parentID + " exists.", "parentID");
            if (weight < 1)
                throw
                    new ArgumentOutOfRangeException("weight", "weight must be 1 or higher.");
            int id = 0;
            while (Instance.indicators.ContainsKey(id)) // create a unique identifier
                id++;
            var indicator = FormatterServices.GetUninitializedObject(typeof(Indicator)) as Indicator;
            IIndicator starter = indicator;
            starter.Start(id, totalSteps, name, stepLabel, weight);
            Instance.indicators.Add(id, indicator);
            Instance.AddedInstance?.Invoke(indicator);
            if (parentID >= 0)
                starter.SetParent(parentID);
            return
                id;
        }

        /// <summary>
        /// Unregisters a previously registered <see cref="Indicator"/> finish callback.
        /// </summary>
        /// <param name="id">The <see cref="Indicator"/>'s unique ID.</param>
        public static void UnregisterFinishCallback(int id)
        {
            if (!Exists(id))
                throw
                    new ArgumentException("No Progress.Indicator with this ID value exists.", "id");
            Instance.indicators[id].UnregisterFinishCallback();
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////

        private float CalculateGlobalProgress()
        {
            var totalProgress = 0f;
            foreach (Indicator indicator in Instance.indicators.Values)
                totalProgress += indicator.Progress;
            return
                totalProgress / Instance.indicators.Count;
        }

        private bool HasRunningIndicators()
        {
            foreach(Indicator indicator in Instance.indicators.Values)
                if (indicator.Running)
                    return
                        true;
            return
                false;
        }

        private interface IIndicator
        {
            void RecalculateProgress();
            void SetParent(int parentID);
            void Start(int id, string name, int weight);
            void Start(int id, int totalSteps, string name, string stepLabel, int weight);
        }

        private static void InvokeUpdated(Indicator indicator)
        {
            if(Exists(indicator.ID))
                Instance.UpdatedInstance?.Invoke(indicator);
        }

        private static void Remove(Indicator indicator)
        {
            if (Exists(indicator.ID))
            {
                if (indicator.HasChildren)
                    foreach (Indicator childIndicator in indicator.Children) 
                        Remove(childIndicator);
                Instance.indicators.Remove(indicator.ID);
                Instance.RemovedInstance?.Invoke(indicator);
                if (indicator.HasParent && !indicator.Parent.Finished)
                {
                    IIndicator parent = indicator.Parent;
                    parent.RecalculateProgress();
                }
            }
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// An indicator that can be used to tracks progress on some kind of arbitrary task.
        /// </summary>
        public sealed class Indicator : IIndicator
        {
            /// <summary>
            /// The number of children the <see cref="Indicator"/> has.
            /// </summary>
            public int ChildCount => HasChildren ? childIndicators.Count : 0;

            /// <summary>
            /// Allows for enumeration over the <see cref="Indicator"/>'s chidren, assuming it has any.
            /// <para>This property returns null if the <see cref="Indicator"/> has no children.</para>
            /// </summary>
            public IEnumerable<Indicator> Children => Exists && HasChildren ? childIndicators : null;

            /// <summary>
            /// Returns the current step for this <see cref="Indicator"/>.
            /// </summary>
            public int CurrentStep { get; private set; }

            /// <summary>
            /// Checks whether the <see cref="Indicator"/> exists. Removed <see cref="Indicator"/>s are considered nonexistant.
            /// </summary>
            public bool Exists => Exists(ID);

            /// <summary>
            /// Returns true if the <see cref="Indicator"/> is finished, but not-yet-removed.
            /// </summary>
            public bool Finished => started && Exists && Status == IndicatorStatus.Finished;

            /// <summary>
            /// Returns the <see cref="Indicator"/>'s unique identifier.
            /// </summary>
            public int ID { get; private set; }

            /// <summary>
            /// Returns whether the <see cref="Indicator"/> is the parent of at least one other <see cref="Indicator"/>.
            /// </summary>
            public bool HasChildren => childIndicators != null && childIndicators.Count > 0;

            /// <summary>
            /// Returns whether the <see cref="Indicator"/> is the child of another <see cref="Indicator"/>.
            /// </summary>
            public bool HasParent => ParentID >= 0 && Parent != null;

            /// <summary>
            /// Returns the <see cref="Indicator"/>'s name.
            /// </summary>
            public string Name { get; private set; }

            /// <summary>
            /// Get the <see cref="Indicator"/>'s parent, if any.
            /// </summary>
            public Indicator Parent => GetIndicatorById(ParentID);

            /// <summary>
            /// Returns the unique ID of the <see cref="Indicator"/>'s parent, or -1 if the <see cref="Indicator"/> is not a child of another <see cref="Indicator"/>.
            /// </summary>
            public int ParentID { get; private set; }

            /// <summary>
            /// Returns the <see cref="Indicator"/>'s progress value.
            /// </summary>
            public float Progress => (!Exists || !started) ? -1f : StepBased ? (float)CurrentStep / TotalSteps : reportedProgress;

            /// <summary>
            /// Returns the topmost <see cref="Indicator"/> in the <see cref="Indicator"/>'s hierarchy.
            /// </summary>
            public Indicator Root => FindRoot();

            /// <summary>
            /// Returns true if the <see cref="Indicator"/> is running.
            /// </summary>
            public bool Running => started && (Status == IndicatorStatus.Started || Status == IndicatorStatus.InProgress);

            /// <summary>
            /// Returns the timewhen the <see cref="Indicator"/> started.
            /// </summary>
            public DateTime StartTime { get; private set; }

            /// <summary>
            /// Returns the <see cref="Indicator"/>'s status.
            /// </summary>
            public IndicatorStatus Status
            {
                get
                {
                    if(!started)
                        throw
                            new InvalidOperationException(IndicatorClass + " was not started.");
                    var progress = Progress;
                    return 
                        progress <= 0f ? 
                            IndicatorStatus.Started
                            : progress >= 1f ? 
                                IndicatorStatus.Finished
                                : IndicatorStatus.InProgress;
                }
            }

            /// <summary>
            /// Returns true if the <see cref="Indicator"/>'s progress is step-based.
            /// </summary>
            public bool StepBased => TotalSteps >= 1;

            /// <summary>
            /// Returns the label that displays the <see cref="Indicator"/>'s steps.
            /// </summary>
            public string StepLabel { get; private set; }

            /// <summary>
            /// Returns the total number of steps for this <see cref="Indicator"/>.
            /// </summary>
            public int TotalSteps { get; private set; }

            /// <summary>
            /// Returns the last time the <see cref="Indicator"/> was updated.
            /// </summary>
            public DateTime UpdateTime { get; private set; }

            /// <summary>
            /// The <see cref="Indicator"/>'s weight value. Used to compute the progress value of a parent <see cref="Indicator"/>
            /// with one or more child <see cref="Indicator"/>s.
            /// </summary>
            public int Weight { get; private set; }

            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////

            private IIndicator IParent => Parent;
            private IIndicator IThis => this;
            private bool ParentShouldRecalculate => (HasParent && !Parent.Finished);

            private event Action<Indicator> FinishCallback;
            private float reportedProgress;
            private bool started;
            private List<Indicator> childIndicators;

            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////

            /// <summary>
            /// No public constructor. The Progress class uses <see cref="FormatterServices.GetUninitializedObject"/> as a workaround
            /// to instantiate <see cref="Indicator"/>s.
            /// </summary>
            private Indicator() {}

            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////

            /// <summary>
            /// Marks the <see cref="Indicator"/> as finished.
            /// <para><see cref="Indicator"/>s without a parent are automatically removed upon being finished.</para>
            /// </summary>
            public void Finish()
            {
                if (!started)
                    throw
                        new InvalidOperationException(IndicatorClass + " was not started.");
                if (!Exists)
                    throw
                        new InvalidOperationException(IndicatorClass + " no longer exists.");
                if(Status != IndicatorStatus.Finished)
                {
                    if (StepBased)
                        CurrentStep = TotalSteps;
                    else
                        reportedProgress = 1f;
                    if (HasChildren) 
                        foreach (Indicator child in childIndicators) 
                            child.Finish();
                    UpdateTime = DateTime.Now;
                    FinishCallback?.Invoke(this);
                    FinishCallback = null;
                    InvokeUpdated(this);
                    if (!HasParent)
                        Remove(this);
                    else if (!Parent.Finished)
                        IParent.RecalculateProgress();
                }
            }

            /// <summary>
            /// Registers a callback that is called when the <see cref="Indicator"/> is finished.
            /// </summary>
            /// <param name="callback">This method is called when the <see cref="Indicator"/> is finished.</param>
            public void RegisterFinishCallback(Action<Indicator> callback)
            {
                if (!Exists) 
                    throw
                        new InvalidOperationException(IndicatorClass + " no longer exists.");
                FinishCallback = callback ??
                    throw
                        new ArgumentNullException("callback");
            }

            /// <summary>
            /// Reports the <see cref="Indicator"/>'s current status.
            /// <para>Reporting a progress value of 1f will cause the <see cref="Indicator"/> to finish.</para>
            /// </summary>
            /// <param name="progress">A new progress value between 0 and 1.</param>
            public void Report(float progress)
            {
                if (!Exists)
                    throw
                        new InvalidOperationException(IndicatorClass + " no longer exists.");
                if (!Running)
                    throw
                        new InvalidOperationException("Cannot report progress on a " + IndicatorClass + " that isn't running.");
                if (HasChildren)
                    throw
                        new InvalidOperationException("Cannot report progress on a " + IndicatorClass + " that has one or more child " + 
                        IndicatorClass + "s.");
                if (progress < 0f || progress > 1f)
                    throw
                        new ArgumentOutOfRangeException("newProgress", "newProgress must be a value between 0f and 1f");
                if(reportedProgress != progress || StepBased)
                {
                    reportedProgress = progress;
                    CurrentStep = 0;
                    TotalSteps = -1;
                    if (Progress == 1f)
                        Finish();
                    else
                    {
                        UpdateTime = DateTime.Now;
                        InvokeUpdated(this);
                        if (ParentShouldRecalculate)
                            IParent.RecalculateProgress();
                    }
                }
            }

            /// <summary>
            /// Reports the <see cref="Indicator"/>'s current status.
            /// <para>Reporting a <paramref name="currentStep"/> value matching <paramref name="totalSteps"/> will cause the <see cref="Indicator"/> to finish.</para>
            /// </summary>
            /// <param name="currentStep">An updated current step.</param>
            /// <param name="totalSteps">An updated total number of steps. (Must be 1 or greater.)</param>
            public void Report(int currentStep, int totalSteps)
            {
                if (!Exists)
                    throw
                        new InvalidOperationException(IndicatorClass + " no longer exists.");
                if (!Running)
                    throw
                        new InvalidOperationException("Cannot report progress on a " + IndicatorClass + " that isn't running.");
                if (totalSteps < 1)
                    throw
                        new ArgumentOutOfRangeException("newtotalSteps", "newTotalSteps must be 1 or greater.");
                if (currentStep > totalSteps) 
                    throw
                        new ArgumentOutOfRangeException("newCurrentTurnStep", "newCurrentTurnStep cannot be greater than newTotalSteps");
                if(currentStep != CurrentStep || totalSteps != TotalSteps)
                {
                    reportedProgress = 0f;
                    CurrentStep = currentStep;
                    TotalSteps = totalSteps;
                    if (Progress == 1f)
                        Finish();
                    else
                    {
                        UpdateTime = DateTime.Now;
                        InvokeUpdated(this);
                        if (ParentShouldRecalculate)
                            IParent.RecalculateProgress();
                    }
                }
            }

            /// <summary>
            /// Sets the label that displays the <see cref="Indicator"/>'s steps.
            /// </summary>
            /// <param name="stepLabel"></param>
            public void SetStepLabel(string stepLabel)
            {
                if (!Exists)
                    throw
                        new InvalidOperationException(IndicatorClass + " no longer exists.");
                if (string.IsNullOrEmpty(stepLabel))
                    StepLabel = "";
                else
                    StepLabel = stepLabel;
            }

            /// <summary>
            /// Returns the <see cref="Indicator"/>'s information as a string of text.
            /// </summary>
            /// <returns>A pretty <see cref="string"/> detailing the <see cref="Indicator"/>'s ID, name, status, progress, and weight.</returns>
            public override string ToString()
            {
                if (!Exists)
                    throw
                        new InvalidOperationException(IndicatorClass + " no longer exists.");
                return
                    StepBased ? 
                        "[ " + ID + " | " + Name + " | " + Status + " | Progress: " + Math.Round(Progress * 100, 2) + "% (" +
                            CurrentStep + "/" + TotalSteps + " " + StepLabel + ") | Weight: " + Weight + " ]"
                        : "[ " + ID + " | " + Name + " | " + Status + " | Progress: " + Math.Round(Progress * 100, 2) + "% | Weight: " + Weight + " ]";
            }

            /// <summary>
            /// Unregisters a previously registered <see cref="Indicator"/> finish callback.
            /// </summary>
            public void UnregisterFinishCallback()
            {
                if (!Exists)
                    throw
                        new InvalidOperationException(IndicatorClass + " no longer exists.");
                FinishCallback = null;
            }

            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////

            private void AddChild(Indicator indicator)
            {
                if (childIndicators == null)
                    childIndicators = new List<Indicator>();
                childIndicators.Add(indicator);
                if(Progress > 0f)
                    IThis.RecalculateProgress();
            }

            private Indicator FindRoot()
            {
                Indicator currentIndicator = this;
                while (currentIndicator.HasParent)
                    currentIndicator = currentIndicator.Parent;
                return
                    currentIndicator;
            }

            void IIndicator.RecalculateProgress()
            {
                var totalWeight = 0;
                var totalProgress = 0f;
                foreach (Indicator child in childIndicators)
                    totalWeight += child.Weight;
                foreach (Indicator child in childIndicators)
                    totalProgress += child.Progress * ((float)child.Weight / totalWeight);
                if (totalProgress == 1f)
                    Finish();
                else
                {
                    reportedProgress = totalProgress;
                    CurrentStep = 0;
                    TotalSteps = -1;
                    UpdateTime = DateTime.Now;
                    InvokeUpdated(this);
                    if (ParentShouldRecalculate)
                        IParent.RecalculateProgress();
                }
            }

            void IIndicator.SetParent(int parentID)
            {
                if (parentID >= 0 && !Exists(parentID))
                    throw
                        new ArgumentException("No " + IndicatorClass + " with ID " + parentID + " exists.", "parentID");
                if (parentID == ID)
                    throw
                        new ArgumentException("The values for parentID and ID cannot be identical.", "parentID");
                ParentID = parentID;
                var parent = GetIndicatorById(parentID);
                parent.AddChild(this);
            }

            void IIndicator.Start(int id, string name, int weight)
            {
                if (started) 
                    throw
                        new InvalidOperationException(IndicatorClass + " cannot be started a second time.");
                if (id < 0) 
                    throw
                        new ArgumentOutOfRangeException("id", "id cannot be negative.");
                if (Exists(id)) 
                    throw
                        new ArgumentException("A " + IndicatorClass + " with ID " + id + " already exists.", "id");
                if (weight < 1) 
                    throw
                        new ArgumentOutOfRangeException("weight", "weight must be 1 or higher.");
                started = true;
                ID = id;
                Name = string.IsNullOrEmpty(name) ? "" : name;
                ParentID = -1;
                Weight = weight;
                StartTime = DateTime.Now;
                UpdateTime = StartTime;
                FinishCallback = null;
                reportedProgress = 0f;
                CurrentStep = 0;
                TotalSteps = -1;
                StepLabel = "";
            }

            void IIndicator.Start(int id, int totalSteps, string name, string stepLabel, int weight)
            {
                if (started) 
                    throw
                        new InvalidOperationException(IndicatorClass + " cannot be started a second time.");
                if (id < 0) 
                    throw
                        new ArgumentOutOfRangeException("id", "id cannot be negative.");
                if (Exists(id)) 
                    throw
                        new ArgumentException("A " + IndicatorClass + " with ID " + id + " already exists.", "id");
                if (totalSteps < 1) 
                    throw
                        new ArgumentOutOfRangeException("totalSteps", "totalSteps must be 1 or greater.");
                if (weight < 1) 
                    throw
                        new ArgumentOutOfRangeException("weight", "weight must be 1 or higher.");
                started = true;
                ID = id;
                Name = string.IsNullOrEmpty(name) ? "" : name;
                ParentID = -1;
                Weight = weight;
                StartTime = DateTime.Now;
                UpdateTime = StartTime;
                FinishCallback = null;
                reportedProgress = 0f;
                CurrentStep = 0;
                TotalSteps = totalSteps;
                StepLabel = stepLabel;
            }
        }
    }
}
