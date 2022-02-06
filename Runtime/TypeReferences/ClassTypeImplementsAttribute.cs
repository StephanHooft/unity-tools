namespace StephanHooft.TypeReferences
{
	/// <summary>
	/// Constraint that allows selection of classes that implement a specific interface
	/// when selecting a <see cref="ClassTypeReference"/> with the Unity inspector.
	/// </summary>
	[System.AttributeUsage(System.AttributeTargets.Field, AllowMultiple = false)]
	public sealed class ClassTypeImplementsAttribute : ClassTypeConstraintAttribute
	{
		#region Properties

		/// <summary>
		/// Gets the <see cref="Type"/> of interface that selectable classes must implement.
		/// </summary>
		public System.Type InterfaceType { get; private set; }

		////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		#endregion
		#region Fields

		////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		#endregion
		#region Constructors and Finaliser

		/// <summary>
		/// Creates a new <see cref="ClassTypeImplementsAttribute"/>.
		/// </summary>
		public ClassTypeImplementsAttribute()
		{ }

		/// <summary>
		/// Creates a new <see cref="ClassTypeImplementsAttribute"/>.
		/// </summary>
		/// <param name="interfaceType">Type of interface that selectable classes must implement.</param>
		public ClassTypeImplementsAttribute(System.Type interfaceType) =>
			InterfaceType = interfaceType;

		~ClassTypeImplementsAttribute()
		{ }

		////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		#endregion
		#region Methods

		/// <inheritdoc/>
		public override bool IsConstraintSatisfied(System.Type type)
		{
			if (base.IsConstraintSatisfied(type))
				foreach (var interfaceType in type.GetInterfaces())
					if (interfaceType == InterfaceType)
						return
							true;
			return
				false;
		}

		////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		#endregion
	}
}
