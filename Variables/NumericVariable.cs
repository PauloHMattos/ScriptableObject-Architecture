namespace ScriptableObjectArchitecture.Variables
{
    public abstract class NumericVariable<TType, TVariable> : BaseVariable<TType> where TVariable : NumericVariable<TType, TVariable>
    {
        public abstract void Add(TType other);
        public abstract void Subtract(TType other);
        //public abstract void Multiply(TType other);
        public abstract void Add(TVariable other);
        public abstract void Subtract(TVariable other);
        //public abstract void Multiply(TVariable other);



        public void SetValue(TVariable variable)
        {
            base.SetValue(variable.Value);
        }
    }
}