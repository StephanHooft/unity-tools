using System;
using UnityEngine;

namespace StephanHooft.TypeReferences
{
	/// <summary>
	/// Base class for class selection constraints that can be applied when selecting
	/// a <see cref="ClassTypeReference"/> with the Unity inspector.
	/// </summary>
	public abstract class ClassTypeConstraintAttribute : PropertyAttribute
	{
		#region Properties

		/// <summary>
		/// Gets or sets grouping of selectable classes. Defaults to <see cref="ClassGrouping.ByNamespaceFlat"/>
		/// unless explicitly specified.
		/// </summary>
		public ClassGrouping Grouping
		{
			get => _grouping;
			set => _grouping = value;
		}

		/// <summary>
		/// Gets or sets whether abstract classes can be selected from drop-down.
		/// Defaults to a value of <c>false</c> unless explicitly specified.
		/// </summary>
		public bool AllowAbstract
		{
			get => _allowAbstract;
			set => _allowAbstract = value;
		}

		////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		#endregion
		#region Fields

		private ClassGrouping _grouping = ClassGrouping.ByNamespaceFlat;
		private bool _allowAbstract = false;

		////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		#endregion
		#region Methods

		/// <summary>
		/// Determines whether the specified class <see cref="Type"/> satisfies filter constraint.
		/// </summary>
		/// <param name="type"><see cref="Type"/> to test.</param>
		/// <returns>
		/// A <see cref="bool"/> value indicating if the <see cref="Type"/> specified by <paramref name="type"/>
		/// satisfies this constraint and should thus be selectable.
		/// </returns>
		public virtual bool IsConstraintSatisfied(Type type) =>
			AllowAbstract || !type.IsAbstract;

		////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		#endregion
	}
}
