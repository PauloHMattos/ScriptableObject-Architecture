﻿using ScriptableObjectArchitecture.Variables;

namespace ScriptableObjectArchitecture.References
{
    [System.Serializable]
    public sealed class IntReference : BaseReference<int, IntVariable>
    {
        public IntReference() : base() { }
        public IntReference(int value) : base(value) { }
    } 
}