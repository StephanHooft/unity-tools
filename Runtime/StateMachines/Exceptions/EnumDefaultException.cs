namespace StephanHooft.StateMachines.Exceptions
{
    [System.Serializable]
    public class EnumDefaultException<TEnum> : System.Exception where TEnum : System.Enum
    {
        public EnumDefaultException(TEnum enumValue)
            : base(string.Format("Enum {0}'s default value '{1}' may not be used here.",
                typeof(TEnum).Name, enumValue.ToString())) { }
    }
}
