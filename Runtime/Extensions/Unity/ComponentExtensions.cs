using StephanHooft.EditorSafe;
using StephanHooft.Exceptions;
using UnityEngine;

namespace StephanHooft.Extensions
{
    public static class ComponentExtensions
    {
        /// <summary>
        /// Adds a <typeparamref name="TComponent"/> to the <see cref="Component"/>'s <see cref="GameObject"/>,
        /// and replaces an existing one if found.
        /// </summary>
        /// <returns>A <typeparamref name="TComponent"/>.</returns>
        public static TComponent AddComponent<TComponent>(this Component component) where TComponent: Component
        {
            return
                component.gameObject.AddComponent<TComponent>();
        }

        /// <summary>
        /// Adds a <typeparamref name="TComponent"/> to the <see cref="Component"/>'s <see cref="GameObject"/>,
        /// and replaces an existing one if found.
        /// </summary>
        /// <returns>A <typeparamref name="TComponent"/>.</returns>
        public static TComponent AddOrReplaceComponent<TComponent>
            (this Component component) where TComponent : Component
        {
            if (component.TryGetComponent(out TComponent foundComponent))
                EditModeSafe.Destroy(foundComponent);
            foundComponent = component.gameObject.AddComponent<TComponent>();
            return
                foundComponent;
        }

        /// <summary>
        /// Destroys all other <see cref="Component"/>s of the same <see cref="Type"/> in the
        /// hierarchy of the <see cref="GameObject"/> that the <see cref="Component"/> is attached to.
        /// </summary>
        public static void DestroyOtherComponentsOfSameTypeInHierarchy(this Component component)
        {
            var type = component.GetType();
            var components = component.transform.root.GetComponentsInChildren(type, true);
            for (int i = 0; i < components.Length; i++)
                if (components[i] != component)
                    EditModeSafe.Destroy(components[i]);
        }

        /// <summary>
        /// Gets a <typeparamref name="TComponent"/> from the <see cref="Component"/>'s 
        /// <see cref="GameObject"/>'s hierarchy.
        /// </summary>
        /// <returns>A <typeparamref name="TComponent"/>, or <see cref="null"/> if none are
        /// found.</returns>
        public static TComponent GetComponentInHierarchy<TComponent>(this Component component)
            where TComponent : Component
        {
            return
                component.transform.root.GetComponentInChildren<TComponent>();
        }

        /// <summary>
        /// Gets all <typeparamref name="TComponent"/>s from the <see cref="Component"/>'s 
        /// <see cref="GameObject"/>'s hierarchy.
        /// </summary>
        /// <returns>A <typeparamref name="TComponent"/>[].</returns>
        public static TComponent[] GetComponentsInHierarchy<TComponent>(this Component component)
            where TComponent : Component
        {
            return
                component.transform.root.GetComponentsInChildren<TComponent>();
        }

        /// <summary>
        /// Gets a <typeparamref name="TComponent"/> from the <see cref="Component"/>'s <see cref="GameObject"/>,
        /// and throws a <see cref="ComponentNotFoundException{TComponent}"/> if none are found.
        /// </summary>
        /// <param name="destroyGameObjectOnFailure">Set to true to destroy the <see cref="Component"/>'s 
        /// <see cref="GameObject"/> if no <typeparamref name="TComponent"/> is found.</param>
        /// <returns>A <typeparamref name="TComponent"/>.</returns>
        public static TComponent GetEssentialComponent<TComponent>
            (this Component component, bool destroyGameObjectOnFailure = false) where TComponent : Component
        {
            if (component.TryGetComponent(out TComponent foundComponent))
                return
                    foundComponent;
            else
            {
                if (destroyGameObjectOnFailure)
                    EditModeSafe.Destroy(component.gameObject);
                throw
                    new ComponentNotFoundException<TComponent>();
            }
        }

        /// <summary>
        /// Gets a <typeparamref name="TComponent"/> from the <see cref="Component"/>'s <see cref="GameObject"/>
        /// or its children, and throws a <see cref="ComponentNotFoundException{TComponent}"/> if none are found.
        /// </summary>
        /// <param name="destroyGameObjectOnFailure">Set to <see cref="true"/> to destroy the 
        /// <see cref="Component"/>'s <see cref="GameObject"/> if no <typeparamref name="TComponent"/>
        /// is found.</param>
        /// <returns>A <typeparamref name="TComponent"/>.</returns>
        public static TComponent GetEssentialComponentInChildren<TComponent>
            (this Component component, bool destroyGameObjectOnFailure = false) where TComponent : Component
        {
            var otherComponent = component.GetComponentInChildren<TComponent>();
            if (otherComponent != null)
                return
                    otherComponent;
            else
            {
                if (destroyGameObjectOnFailure)
                    EditModeSafe.Destroy(component.gameObject);
                throw
                    new ComponentNotFoundException<TComponent>();
            }
        }

        /// <summary>
        /// Gets a <typeparamref name="TComponent"/> from the <see cref="Component"/>'s<see cref="GameObject"/>'s
        /// hierarchy, and throws a <see cref="ComponentNotFoundException{TComponent}"/> if none are found.
        /// </summary>
        /// <param name="destroyGameObjectOnFailure">Set to <see cref="true"/> to destroy the 
        /// <see cref="Component"/>'s <see cref="GameObject"/> if no <typeparamref name="TComponent"/>
        /// is found.</param>
        /// <returns>A <typeparamref name="TComponent"/>.</returns>
        public static TComponent GetEssentialComponentInHierarchy<TComponent>
            (this Component component, bool destroyGameObjectOnFailure = false)
            where TComponent : Component
        {
            var foundComponent = component.transform.root.GetComponentInChildren<TComponent>();
            if (foundComponent != null)
                return
                    foundComponent;
            else
            {
                if (destroyGameObjectOnFailure)
                    EditModeSafe.Destroy(component.gameObject);
                throw
                    new ComponentNotFoundException<TComponent>();
            }
        }

        /// <summary>
        /// Gets a <typeparamref name="TComponent"/> from the <see cref="Component"/>'s <see cref="GameObject"/>
        /// or its parents, and throws a <see cref="ComponentNotFoundException{TComponent}"/> if none are found.
        /// </summary>
        /// <param name="destroyGameObjectOnFailure">Set to <see cref="true"/> to destroy the 
        /// <see cref="Component"/>'s <see cref="GameObject"/> if no <typeparamref name="TComponent"/>
        /// is found.</param>
        /// <returns>A <typeparamref name="TComponent"/>.</returns>
        public static TComponent GetEssentialComponentInParent<TComponent>
            (this Component component, bool destroyGameObjectOnFailure = false) where TComponent : Component
        {
            var foundComponent = component.GetComponentInParent<TComponent>();
            if (foundComponent != null)
                return
                    foundComponent;
            else
            {
                if (destroyGameObjectOnFailure)
                    EditModeSafe.Destroy(component.gameObject);
                throw
                    new ComponentNotFoundException<TComponent>();
            }
        }

        /// <summary>
        /// Gets a <typeparamref name="TComponent"/> from the <see cref="Component"/>'s
        /// <see cref="GameObject"/>, and adds one if none are found.
        /// </summary>
        /// <returns>A <typeparamref name="TComponent"/>.</returns>
        public static TComponent GetOrAddComponent<TComponent>
            (this Component component) where TComponent : Component
        {
            if (!component.TryGetComponent(out TComponent foundComponent))
                foundComponent = component.gameObject.AddComponent<TComponent>();
            return
                foundComponent;
        }

        /// <summary>
        /// Returns true if the <see cref="Component"/>'s <see cref="GameObject"/> has a
        /// <typeparamref name="TComponent"/>.
        /// </summary>
        /// <returns>True if the <see cref="Component"/>'s <see cref="GameObject"/> has a
        /// <see cref="Component"/> of <see cref="Type"/> <typeparamref name="TComponent"/>.</returns>
        public static bool HasComponent<TComponent>(this Component component) where TComponent : Component
        {
            return
                component.GetComponent<TComponent>() != null;
        }

        /// <summary>
        /// Returns <see cref="true"/> if the <see cref="Component"/> is the only one of its
        /// <see cref="Type"/> in the hierarchy of the <see cref="GameObject"/> it's attached to.
        /// </summary>
        /// <returns><see cref="true"/> if the <see cref="Component"/> is the only one of its
        /// <see cref="Type"/>.</returns>
        public static bool IsOnlyComponentOfTypeInHierarchy(this Component component)
        {
            var type = component.GetType();
            var components = component.transform.root.GetComponentsInChildren(type, true);
            return
                components.Length == 1;
        }

        /// <summary>
        /// Returns the <see cref="Component"/>, but only if it's the only one of its <see cref="Type"/> 
        /// in the hierarchy of the <see cref="GameObject"/> it's attached to.
        /// <para>If another <see cref="Component"/> of the same <see cref="Type"/> is found in the
        /// <see cref="GameObject"/>'s hierarchy, an <see cref="InvalidOperationException"/>
        ///is thrown.</para>
        /// </summary>
        /// <returns>The <see cref="Component"/>.</returns>
        public static Component OnlyComponentOfTypeInHierarchy
            (this Component component)
        {
            var type = component.GetType();
            var components = component.transform.root.GetComponentsInChildren(type, true);
            if(components.Length > 1)
            {
                EditModeSafe.Destroy(component);
                throw
                    new ComponentNotPermittedException(type);
            }
            return
                component;
        }
    }
}
