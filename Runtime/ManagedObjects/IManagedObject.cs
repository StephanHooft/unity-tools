namespace StephanHooft.ManagedObjects
{
    /// <summary>
    /// An object managed by an <see cref="IObjectManager"/>.
    /// </summary>
    public interface IManagedObject
    {
        #region Interface Properties

        /// <summary>
        /// Whether the <see cref="IManagedObject"/> has had its <see cref="IObjectManager"/> set through
        /// <see cref="Initialise(IObjectManager)"/>.
        /// </summary>
        public bool Initialised { get; }

        /// <summary>
        /// The <see cref="IManagedObject"/>'s manager.
        /// </summary>
        IObjectManager Manager { get; }

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #endregion
        #region Interface Methods

        /// <summary>
        /// Sets the <see cref="IManagedObject"/>'s <see cref="IObjectManager"/>.
        /// </summary>
        /// <param name="manager">The <see cref="IObjectManager"/> to set.</param>
        void Initialise(IObjectManager manager);

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #endregion
    }
}
