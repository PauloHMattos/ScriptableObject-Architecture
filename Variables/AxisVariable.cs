﻿using ScriptableObjectArchitecture.Attributes;
using ScriptableObjectArchitecture.Utility;
using UnityEngine;

namespace ScriptableObjectArchitecture.Variables
{
    [CreateAssetMenu(
        fileName = "AxisVariable.asset",
        menuName = SOArchitecture_Utility.VARIABLE_SUBMENU + "Axis",
        order = 124)]
    public class AxisVariable : ReadOnlyFloatVariable
    {
        [Group("Axis Config"), SerializeField]
        private string _axisName = "Horizontal";
        [SerializeField]
        private bool _raw = false;

        public override bool Clampable { get { return false; } }

        public override float Value
        {
            get
            {
                try
                {
                    if (_raw)
                    {
                        _value = Input.GetAxisRaw(_axisName);
                    }
                    else
                    {
                        _value = Input.GetAxis(_axisName);
                    }
                }
                catch (System.ArgumentException)
                {
                    _value = float.NaN;
                    //Debug.LogException(e);
                }
                return _value;
            }
        }
    }
}