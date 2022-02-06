using UnityEngine;

namespace StephanHooft.TypeReferences
{
	/// <summary>
	/// Reference to a class <see cref="System.Type"/> with support for Unity serialization.
	/// <remarks><para>Liberally adapted from the work found at:
	/// https://bitbucket.org/rotorz/classtypereference-for-unity/ .</para></remarks>
	/// </summary>
	[System.Serializable]
	public struct ClassTypeReference : ISerializationCallbackReceiver
	{
		#region Properties

		/// <summary>
		/// The class <see cref="System.Type"/> of the <see cref="ClassTypeReference"/>.
		/// </summary>
		public System.Type Type => type;

		////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		#endregion
		#region Fields

		private System.Type type;

		[SerializeField]
		private string typeRef;

		////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		#endregion
		#region Constructors

		/// <summary>
		/// Creates a new <see cref="ClassTypeReference"/>.
		/// </summary>
		/// <param name="assemblyQualifiedClassName">Assembly qualified class name.</param>
		/// <exception cref="ArgumentException">
		/// If <paramref name="assemblyQualifiedClassName"/> is not a <see cref="System.Type"/>.
		/// </exception>
		public ClassTypeReference(string assemblyQualifiedClassName)
		{
			var type = System.Type.GetType(assemblyQualifiedClassName);
			if (type != null && !type.IsClass)
				throw
					new System.ArgumentException(string.Format("'{0}' is not a class type.",
					assemblyQualifiedClassName), "assemblyQualifiedClassName");
			this.type = type;
			typeRef = GetClassTypeRef(type);
		}

		/// <summary>
		/// Creates a new <see cref="ClassTypeReference"/>.
		/// </summary>
		/// <param name="type">Class <see cref="System.Type"/>.</param>
		/// <exception cref="ArgumentException">
		/// If <paramref name="type"/> is not a <see cref="System.Type"/>.
		/// </exception>
		public ClassTypeReference(System.Type type)
		{
			if (type != null && !type.IsClass)
				throw
					new System.ArgumentException(string.Format("'{0}' is not a class type.",
					type.FullName), "type");
			this.type = type;
			typeRef = GetClassTypeRef(type);
		}

		////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		#endregion
		#region Operators

		public static implicit operator string(ClassTypeReference typeReference) =>
			typeReference.typeRef;

		public static implicit operator System.Type(ClassTypeReference typeReference) =>
			typeReference.Type;
		
		public static implicit operator ClassTypeReference(System.Type type) =>
			new ClassTypeReference(type);
		
		public override string ToString() =>
			Type != null ? Type.FullName : "(None)";

		////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		#endregion
		#region Static Methods

		public static string GetClassTypeRef(System.Type type) =>
			type != null
				? type.FullName + ", " + type.Assembly.GetName().Name
				: "";

		////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		#endregion
		#region ISerializationCallbackReceiver Implementation

		void ISerializationCallbackReceiver.OnAfterDeserialize()
		{
			if (!string.IsNullOrEmpty(typeRef))
			{
				type = System.Type.GetType(typeRef);
				if (type == null)
					Debug.LogWarning(string.Format("'{0}' was referenced but class type was not found.", typeRef));
			}
			else
				type = null;
		}

		void ISerializationCallbackReceiver.OnBeforeSerialize()
		{ }

		#endregion
	}

}
