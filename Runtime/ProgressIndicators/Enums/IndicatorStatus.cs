namespace StephanHooft.ProgressIndicators
{
    /// <summary>
    /// Indicates the status of a <see cref="Progress.Indicator"/>.
    /// </summary>
    public enum IndicatorStatus
    {
        /// <summary>
        /// That the <see cref="Progress.Indicator"/> has started, but no progress has been made.
        /// </summary>
        Started,

        /// <summary>
        /// The <see cref="Progress.Indicator"/> is running and in progress.
        /// </summary>
        InProgress,

        /// <summary>
        /// The <see cref="Progress.Indicator"/> has finished.
        /// </summary>
        Finished
    }
}
