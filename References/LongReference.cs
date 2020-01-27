﻿using ScriptableObjectArchitecture.Variables;

namespace ScriptableObjectArchitecture.References
{
    [System.Serializable]
    public sealed class LongReference : BaseReference<long, LongVariable>
    {
        public LongReference() : base() { }
        public LongReference(long value) : base(value) { }
    } 
}