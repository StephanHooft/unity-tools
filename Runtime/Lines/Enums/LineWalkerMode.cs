namespace StephanHooft.Lines
{
    /// <summary>
    /// Behaviour styles for a <see cref="LineWalker2D"/>.
    /// </summary>
    public enum LineWalkerMode
    {
        /// <summary>
        /// Follow line once and stop once the end has been reached.
        /// </summary>
        Once,

        /// <summary>
        /// Repeatedly follow line by starting over once the end has been reached.
        /// </summary>
        Loop,

        /// <summary>
        /// Repeatedly move back and forth along the line.
        /// </summary>
        PingPong
    }
}
