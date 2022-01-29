using UnityEngine;

namespace StephanHooft.EditorSafe
{
    /// <summary>
    /// A collection of static methods that execute differently while in Edit Mode.
    /// </summary>
    public static class EditModeSafe
    {
        private static bool InPlayMode => Application.isPlaying;

        /// <summary>
        /// A Destroy method that calls <see cref="Object.DestroyImmediate(Object)"/> instead if the application is not playing.
        /// </summary>
        /// <param name="obj">The <see cref="Object"/> to destroy.</param>
        public static void Destroy(Object obj)
        {
            if (InPlayMode)
                Object.Destroy(obj);
            else
                Object.DestroyImmediate(obj);
        }
    }
}
