namespace StephanHooft.StateMachines.Exceptions
{
    [System.Serializable]
    public class StateUnavailableException : System.Exception
    {
        public StateUnavailableException(System.Enum key)
            : base(string.Format("No state with key '{0}' was registered.", key)) { }

        public StateUnavailableException(string name)
            : base(string.Format("No state with name '{0}' was registered.", name)) { }
    }
}
