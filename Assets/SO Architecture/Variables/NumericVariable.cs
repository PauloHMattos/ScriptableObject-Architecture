namespace ScriptableObjectArchitecture
{
    public abstract class NumericVariable<TType, TVariable> : BaseVariable<TType> where TVariable : NumericVariable<TType, TVariable>
    {
        public abstract void Add(TType other);
        public abstract void Subtract(TType other);
        public abstract void Add(TVariable other);
        public abstract void Subtract(TVariable other);
    }
}