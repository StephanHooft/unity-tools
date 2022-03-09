namespace StephanHooft.HybridUpdate
{
    /// <summary>
    /// An interface for <see cref="HybridUpdater"/>s.
    /// </summary>
    public interface IHybridUpdater
    {
        #region Properties

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #endregion
        #region Methods

        /// <summary>
        /// Reports a FixedUpdate call to the <see cref="IHybridUpdater"/>.
        /// </summary>
        void ReportFixedUpdateCall();

        /// <summary>
        /// Registers an object to the <see cref="IHybridUpdater"/> and receives a
        /// <see cref="HybridUpdateCallback"/> in return. The object must hang on to this reference so it can
        /// unregister itself at a later point.
        /// </summary>
        /// <param name="callbackAction">The "HybridUpdate" method that the <see cref="IHybridUpdater"/> will call
        /// whenever it pushes the object to update.</param>
        /// <param name="type">The <see cref="System.Type"/> of the registering object.</param>
        /// <param name="priority">The relative priority of the registering object type.</param>
        /// <returns>A <see cref="HybridUpdateCallback"/> that must be used to report Update and FixedUpdate calls.
        /// </returns>
        HybridUpdateCallback Register(System.Type type, int priority, System.Action<float> callbackAction);

        /// <summary>
        /// Unregisters an object from the <see cref="IHybridUpdater"/>, preventing further callbacks to it.
        /// </summary>
        /// <param name="callback"></param>
        void Unregister(HybridUpdateCallback callback);

        /// <summary>
        /// Reports an Update call to the <see cref="IHybridUpdater"/>.
        /// </summary>
        /// <param name="deltaTime">Pass the current value of <see cref="UnityEngine.Time.deltaTime"/> here.</param>
        void ReportUpdateCall(float deltaTime);

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #endregion
    }
}
