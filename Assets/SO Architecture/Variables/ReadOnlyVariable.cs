namespace ScriptableObjectArchitecture
{
    public abstract class ReadOnlyVariable<TBase> : BaseVariable<TBase>
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

    }
}