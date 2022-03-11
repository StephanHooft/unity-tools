# unity-tools
This repository is a Unity package for C# code to reuse across projects. It contains:
- A "ComponentPool"custom generic Component pooler.
- A "HybridUpdater" system to update custom Monobehaviours whenever FixedUpdate is called, as well as when multiple Update calls occur between FixedUpdates.
- A "LineRendererUpdate" system to manage the updating of a LineRenderer's positions on every Update.
- A "ManagedObjects" set of matching interfaces, for implementing pairs of objects and their managing entities.
- A "ManagedRayCasts" suite of structs to start (and encapsulate the results of) various kinds of raycasts.
- A "MultiKeyDictionary" class.
- A "Progress" class to help various tasks to generically track their progress.
- A "SortKeyDictionary" class.
- A "StateMachines" framework for setting up (finite) state machines.
- A "TypeReferences" system to serialize Type references.
- Custom attributes.
- Extension methods for various standard C#/Unity classes, as well as some classes from this package.
- Tools for drawing and following (segmented) lines in 2D space.