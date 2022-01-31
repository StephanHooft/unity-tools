using System;

namespace StephanHooft.TypeReferences
{
	/// <summary>
	/// Constraint that allows selection of classes that implement a specific interface
	/// when selecting a <see cref="ClassTypeReference"/> with the Unity inspector.
	/// </summary>
	[AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
	public sealed class ClassImplementsAttribute : ClassTypeConstraintAttribute
	{
		/// <summary>
		/// Creates a new <see cref="ClassImplementsAttribute"/>.
		/// </summary>
		public ClassImplementsAttribute()
		{ }

		/// <summary>
		/// Creates a new <see cref="ClassImplementsAttribute"/>.
		/// </summary>
		/// <param name="interfaceType">Type of interface that selectable classes must implement.</param>
		public ClassImplementsAttribute(Type interfaceType) =>		
			InterfaceType = interfaceType;

		/// <summary>
		/// Gets the <see cref="Type"/> of interface that selectable classes must implement.
		/// </summary>
		public Type InterfaceType { get; private set; }

		/// <inheritdoc/>
		public override bool IsConstraintSatisfied(Type type)
		{
			if (base.IsConstraintSatisfied(type))
				foreach (var interfaceType in type.GetInterfaces())
					if (interfaceType == InterfaceType)
						return
							true;
			return
				false;
		}
	}
}
