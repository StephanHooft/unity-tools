namespace StephanHooft.StateMachines.Exceptions
{
    [System.Serializable]
    public class StateDuplicationException : System.Exception
    {
        public StateDuplicationException(System.Enum key)
            : base(string.Format("A state with key '{0}' already exists.", key)) { }

        public StateDuplicationException(System.Type type)
            : base(string.Format("A state of type '{0}' already exists.", type)) { }
    }
}
