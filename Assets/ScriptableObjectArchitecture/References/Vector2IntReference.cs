﻿using ScriptableObjectArchitecture.Variables;
using UnityEngine;

namespace ScriptableObjectArchitecture.References
{
    [System.Serializable]
    public sealed class Vector2IntReference : BaseReference<Vector2Int, Vector2IntVariable>
    {
        public Vector2IntReference() : base() { }
        public Vector2IntReference(Vector2Int value) : base(value) { }
    }
}