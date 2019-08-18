namespace ScriptableObjectArchitecture
{
    public abstract class ReadOnlyFloatVariable : FloatVariable
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