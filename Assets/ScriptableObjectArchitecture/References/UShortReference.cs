﻿using ScriptableObjectArchitecture.Variables;

namespace ScriptableObjectArchitecture.References
{
    [System.Serializable]
    public sealed class UShortReference : BaseReference<ushort, UShortVariable>
    {
        public UShortReference() : base() { }
        public UShortReference(ushort value) : base(value) { }
    } 
}