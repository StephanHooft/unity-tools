namespace StephanHooft.StateMachines.Exceptions
{
    [System.Serializable]
    public class EnumValueUndefinedException<TEnum> : System.Exception where TEnum : System.Enum
    {
        public EnumValueUndefinedException(TEnum enumValue)
            : base(string.Format("The value '{0:N0}' has not been defined for enum {1}.",
                enumValue, typeof(TEnum).Name))
        { }
    }
}
