using ScriptableObjectArchitecture.Variables;

namespace ScriptableObjectArchitecture.References
{
    [System.Serializable]
    public sealed class UIntReference : BaseReference<uint, UIntVariable>
    {
        public UIntReference() : base() { }
        public UIntReference(uint value) : base(value) { }
    } 
}