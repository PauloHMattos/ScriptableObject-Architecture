using ScriptableObjectArchitecture.Variables;

namespace ScriptableObjectArchitecture.References
{
    [System.Serializable]
    public sealed class CharReference : BaseReference<char, CharVariable>
    {
        public CharReference() : base() { }
        public CharReference(char value) : base(value) { }
    } 
}