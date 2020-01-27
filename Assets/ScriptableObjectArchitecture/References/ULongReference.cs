using ScriptableObjectArchitecture.Variables;

namespace ScriptableObjectArchitecture.References
{
    [System.Serializable]
    public sealed class ULongReference : BaseReference<ulong, ULongVariable>
    {
        public ULongReference() : base() { }
        public ULongReference(ulong value) : base(value) { }
    } 
}