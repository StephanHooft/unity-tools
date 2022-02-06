using UnityEngine;

namespace StephanHooft.Exceptions
{
    [System.Serializable]
    public class ComponentNotFoundException<TComponent> : System.Exception where TComponent : Component
    {
        public ComponentNotFoundException()
            : base(string.Format("No Component of type {0} was found.", typeof(TComponent).ToString())) { }
    }
}
