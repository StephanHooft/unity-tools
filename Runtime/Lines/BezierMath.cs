using UnityEngine;

namespace StephanHooft.Lines
{
	/// <summary>
	/// Helper class for interpolation of Bezier curves.
	/// </summary>
	public static class BezierMath
	{
		#region Static Methods

		/// <summary>
		/// Calculate the position of a point <paramref name="t"/> along a Bezier curve through quadratic interpolation. (One control point.)
		/// </summary>
		/// <param name="p0">Start point.</param>
		/// <param name="p1">Control point.</param>
		/// <param name="p2">End point.</param>
		/// <param name="t">Position (0f - 1f) on the curve.</param>
		/// <returns>A <see cref="Vector3"/> position.</returns>
		public static Vector3 GetPoint(Vector3 p0, Vector3 p1, Vector3 p2, float t)
		{
			t = Mathf.Clamp01(t);
			var oneMinusT = 1f - t;
			return
				oneMinusT * oneMinusT * p0 +
				2f * oneMinusT * t * p1 +
				t * t * p2;
		}

		/// <summary>
		/// Calculate the position of a point <paramref name="t"/> along a Bezier curve through cubic interpolation. (Two control points.)
		/// </summary>
		/// <param name="p0">Start point.</param>
		/// <param name="p1">First control point.</param>
		/// <param name="p2">Second control point.</param>
		/// <param name="p3">End point.</param>
		/// <param name="t">Position (0f - 1f) on the curve.</param>
		/// <returns>A <see cref="Vector3"/> position.</returns>
		public static Vector3 GetPoint(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, float t)
		{
			t = Mathf.Clamp01(t);
			var oneMinusT = 1f - t;
			return
				oneMinusT * oneMinusT * oneMinusT * p0 +
				3f * oneMinusT * oneMinusT * t * p1 +
				3f * oneMinusT * t * t * p2 +
				t * t * t * p3;
		}

		/// <summary>
		/// Calculate the derivative (slope) at a point <paramref name="t"/> along a Bezier curve through quadratic interpolation. (One control point.)
		/// </summary>
		/// <param name="p0">Start point.</param>
		/// <param name="p1">Control point.</param>
		/// <param name="p2">End point.</param>
		/// <param name="t">Position (0f - 1f) on the curve.</param>
		/// <returns>A <see cref="Vector3"/> representing the derivative of the Bezier curve af position <paramref name="t"/>.</returns>
		public static Vector3 GetFirstDerivative(Vector3 p0, Vector3 p1, Vector3 p2, float t)
		{
			return
				2f * (1f - t) * (p1 - p0) +
				2f * t * (p2 - p1);
		}

		/// <summary>
		/// Calculate the derivative (slope) at a point <paramref name="t"/> along a Bezier curve through cubic interpolation. (Two control points.)
		/// </summary>
		/// <param name="p0">Start point.</param>
		/// <param name="p1">First control point.</param>
		/// <param name="p2">Second control point.</param>
		/// <param name="p3">End point.</param>
		/// <param name="t">Position (0f - 1f) on the curve.</param>
		/// <returns>A <see cref="Vector3"/> representing the derivative of the Bezier curve af position <paramref name="t"/>.</returns>
		public static Vector3 GetFirstDerivative(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, float t)
		{
			t = Mathf.Clamp01(t);
			var oneMinusT = 1f - t;
			return
				3f * oneMinusT * oneMinusT * (p1 - p0) +
				6f * oneMinusT * t * (p2 - p1) +
				3f * t * t * (p3 - p2);
		}
		////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		#endregion
	}
}
