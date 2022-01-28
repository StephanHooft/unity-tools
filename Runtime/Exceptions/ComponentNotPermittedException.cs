using System;
using UnityEngine;

namespace StephanHooft.Exceptions
{
    [Serializable]
    public class ComponentNotPermittedException : Exception
    {
        public ComponentNotPermittedException()
            : base("The Component is not permitted.") { }

        public ComponentNotPermittedException(Type type)
            : base(string.Format("The Component of type {0} is not permitted.", type.ToString())) { }
    }
}
