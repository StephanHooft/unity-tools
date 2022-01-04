namespace StephanHooft.Lines
{
	/// <summary>
	/// Control point modes for a point on a Bezier curve.
	/// </summary>
	public enum BezierControlPointMode
	{
		/// <summary>
		/// Control points can be positioned independently.
		/// </summary>
		Free,

		/// <summary>
		/// Control points are aligned in a straight line, but may have differing distances from their node.
		/// </summary>
		Aligned,

		/// <summary>
		/// Control points are mirrored with regards to their node.
		/// </summary>
		Mirrored
	}
}
