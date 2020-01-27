using ScriptableObjectArchitecture.Variables;

namespace ScriptableObjectArchitecture.References
{
    [System.Serializable]
    public sealed class FloatReference : BaseReference<float, FloatVariable>
    {
        public FloatReference() : base() { }
        public FloatReference(float value) : base(value) { }
    } 
}