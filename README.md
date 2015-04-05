Unitilities v1.2.1
==================

Set of utility scripts to facilitate work in Unity.

Currently, it includes:
* Extension methods for:
  * GameObjects
  * Primitives and comparable types
  * Lists and arrays
  * Vectors
  * Strings
  * Colors
  * Date/Time
* New data types and structures:
  * HSVColor: for a representation of color using the Hue-Saturation-Value system.
  * Pool: to manage GameObject instances instead of creating/destroying them.
  * Singleton.
  * Timer & ManagedTimer type, useful to do things after a certain time.
  * Tuple (Tuple-2, Tuple-3 and Tuple-4) to hold and bind generic types.
* PlayerPrefs window to view and modify preferences inside the Unity Editor.
* Gizmo-helpers for the Editor.
* Localization

The project is currently divided into these namespaces:
* Unitilities: for basic things. It's everything located under /Core.
* Unitilities.Colors
* Unitilities.Pools
* Unitilities.Tuples
* Unitilities.Debugging
* Unitilities.EditorStuff
* Unitilities.Timers
* Unitilities.Localization
