namespace ScriptableObjectArchitecture
{
    [HelpBox("ReadOnly variables does not trigger changed events", HelpBoxType.Info)]
    public abstract class ReadOnlyFloatVariable : FloatVariable
    {
        public override bool ReadOnly { get => true; }

        public override void Awake()
        {
            base.Awake();
            _readOnly = true;
            _resetWhenStart = false;
        }
    }
}