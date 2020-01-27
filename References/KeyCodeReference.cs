using ScriptableObjectArchitecture.Variables;
using UnityEngine;

namespace ScriptableObjectArchitecture.References
{
    [System.Serializable]
    public sealed class KeyCodeReference : BaseReference<KeyCode, KeyCodeVariable>
    {
        public KeyCodeReference() : base() { }
        public KeyCodeReference(KeyCode value) : base(value) { }
    }
}