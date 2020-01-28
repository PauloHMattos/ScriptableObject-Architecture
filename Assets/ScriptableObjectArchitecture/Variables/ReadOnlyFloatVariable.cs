﻿using ScriptableObjectArchitecture.Attributes;
using UnityEngine;

namespace ScriptableObjectArchitecture.Variables
{
    [HelpBox("ReadOnly variables does not trigger changed events", HelpBoxType.Info)]
    public abstract class ReadOnlyFloatVariable : FloatVariable
    {
        protected override bool FullReadOnly => true;
        public override bool ReadOnly => true;

        public override void Awake()
        {
            base.Awake();
            _readOnly = true;
            _resetWhenStart = false;
            _isClamped = false;
            _raiseWarning = false;
        }
    }
}