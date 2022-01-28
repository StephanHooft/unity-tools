using System;
using UnityEngine;

namespace StephanHooft.Exceptions
{
    [Serializable]
    public class ComponentNotFoundException<TComponent> : Exception where TComponent : Component
    {
        public ComponentNotFoundException()
            : base(string.Format("No Component of type {0} was found.", typeof(TComponent).ToString())) { }
    }
}
