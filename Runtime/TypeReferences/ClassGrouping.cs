using UnityEngine;

/// <summary>
/// Indicates how selectable class type names should be collated in a drop-down menu.
/// </summary>
public enum ClassGrouping
{
	/// <summary>
	/// No grouping, just show class type names in a list; for instance, "Some.Nested.Namespace.SpecialClass".
	/// </summary>
	None,
	/// <summary>
	/// Group classes by namespace and show foldout menus for nested namespaces; for
	/// instance, "Some > Nested > Namespace > SpecialClass".
	/// </summary>
	ByNamespace,
	/// <summary>
	/// Group classes by namespace; for instance, "Some.Nested.Namespace > SpecialClass".
	/// </summary>
	ByNamespaceFlat,
	/// <summary>
	/// Group classes in the same way as Unity does for its component menu. This
	/// grouping method must only be used for <see cref="MonoBehaviour"/> types.
	/// </summary>
	ByAddComponentMenu,
}
