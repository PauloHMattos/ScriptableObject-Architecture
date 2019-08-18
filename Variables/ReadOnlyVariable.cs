namespace ScriptableObjectArchitecture
{
    public abstract class ReadOnlyVariable<TBase, TVariable> : NumericVariable<TBase, TVariable>
        where TVariable : NumericVariable<TBase, TVariable>
    {
        public override bool ReadOnly
        {
            get
            {
                return _readOnly;
            }
        }

        public override void Awake()
        {
            base.Awake();
            _readOnly = true;
            _resetWhenStart = false;
        }

        //public override TBase SetValue(TBase value)
        //{
        //    throw new System.InvalidOperationException();
        //}

        public override void Add(TBase other)
        {
            throw new System.InvalidOperationException();
        }

        public override void Add(TVariable other)
        {
            throw new System.InvalidOperationException();
        }

        public override void Subtract(TBase other)
        {
            throw new System.InvalidOperationException();
        }

        public override void Subtract(TVariable other)
        {
            throw new System.InvalidOperationException();
        }
    }
}