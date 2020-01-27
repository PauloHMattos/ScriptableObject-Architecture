using ScriptableObjectArchitecture.Utility;
using UnityEngine;

namespace ScriptableObjectArchitecture.Variables
{
    [CreateAssetMenu(
        fileName = "SByteVariable.asset",
        menuName = SoArchitectureUtility.ADVANCED_VARIABLE_SUBMENU + "sbyte",
        order = SoArchitectureUtility.ASSET_MENU_ORDER_COLLECTIONS + 15)]
    public class SByteVariable : NumericVariable<sbyte, SByteVariable>
    {
        public override bool Clampable => true;

        protected override sbyte ClampValue(sbyte value)
        {
            if (value.CompareTo(MinClampValue) < 0)
            {
                return MinClampValue;
            }
            else if (value.CompareTo(MaxClampValue) > 0)
            {
                return MaxClampValue;
            }
            else
            {
                return value;
            }
        }

        public override void Add(sbyte other)
        {
            Value += other;
        }

        public override void Subtract(sbyte other)
        {
            Value -= other;
        }

        public override void Add(SByteVariable other)
        {
            Value += other.Value;
        }

        public override void Subtract(SByteVariable other)
        {
            Value -= other.Value;
        }
    } 
}