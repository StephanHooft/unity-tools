using System;

namespace StephanHooft.TypeReferences
{
	/// <summary>
	/// Constraint that allows selection of classes that extend a specific class when
	/// selecting a <see cref="ClassTypeReference"/> with the Unity inspector.
	/// </summary>
	[AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
	public sealed class ClassTypeExtendsAttribute : ClassTypeConstraintAttribute
	{
		#region Properties

		/// <summary>
		/// Gets the <see cref="Type"/> of class that selectable classes must derive from.
		/// </summary>
		public Type BaseType { get; private set; }

		////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		#endregion
		#region Fields

		////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		#endregion
		#region Constructors and Finaliser

		/// <summary>
		/// Creates a new <see cref="ClassTypeExtendsAttribute"/>.
		/// </summary>
		public ClassTypeExtendsAttribute()
		{ }

		/// <summary>
		/// Creates a new <see cref="ClassTypeExtendsAttribute"/>.
		/// </summary>
		/// <param name="baseType">Type of class that selectable classes must derive from.</param>
		public ClassTypeExtendsAttribute(Type baseType) =>
			BaseType = baseType;

		~ClassTypeExtendsAttribute()
		{ }

		////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		#endregion
		#region Static Methods

		////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		#endregion
		#region Methods

		/// <inheritdoc/>
		public override bool IsConstraintSatisfied(Type type) =>
			base.IsConstraintSatisfied(type)
				&& BaseType.IsAssignableFrom(type) && type != BaseType;

		////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		#endregion
	}
}
