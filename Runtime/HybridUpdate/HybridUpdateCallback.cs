using StephanHooft.Extensions;

namespace StephanHooft.HybridUpdate
{
    /// <summary>
    /// A data structure that a <see cref="HybridUpdater"/> uses to keep track of all
    /// <see cref="HybridUpdater.Behaviour"/>s that rely on it for "HybridUpdate" calls.
    /// </summary>
    public class HybridUpdateCallback
    {
        #region Properties

        /// <summary>
        /// The "HybridUpdate" callback method that the <see cref="HybridUpdater"/> calls whenever it updates.
        /// </summary>
        public readonly System.Action<float> action;

        /// <summary>
        /// The <see cref="System.Type"/> of the object that registered the callback.
        /// </summary>
        public readonly System.Type type;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #endregion
        #region Constructor

        /// <summary>
        /// Creates a new <see cref="HybridUpdateCallback"/>.
        /// </summary>
        /// <param name="action">The "HybridUpdate" callback method that the <see cref="HybridUpdater"/> should call
        /// when updating.</param>
        /// <param name="type">The <see cref="System.Type"/> of the object registering the callback.</param>
        public HybridUpdateCallback(System.Action<float> action, System.Type type)
        {
            this.action = action.MustNotBeNull("callback");
            this.type = type.MustNotBeNull("type");
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #endregion
    }
}
