namespace StephanHooft.StateMachines.Exceptions
{
    [System.Serializable]
    public class StateRetrievalException<TEnum> : System.Exception where TEnum : System.Enum
    {
        public StateRetrievalException(TEnum key)
            : base(string.Format("No state with key '{0}' was registered.", key)) { }

        public StateRetrievalException(TEnum key, TEnum expectedKey)
            : base(string.Format("State returned a different key than it did during state machine initialisation." +
                " Expected: {0}. Received: {1}. States should return a constant enum value.", expectedKey, key)) { }
    }
}
