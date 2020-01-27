using ScriptableObjectArchitecture.Utility;
using UnityEngine;

namespace ScriptableObjectArchitecture.Variables
{
    [CreateAssetMenu(
        fileName = "UnsignedLongVariable.asset",
        menuName = SoArchitectureUtility.ADVANCED_VARIABLE_SUBMENU + "ulong",
        order = SoArchitectureUtility.ASSET_MENU_ORDER_COLLECTIONS + 17)]
    public class ULongVariable : NumericVariable<ulong, ULongVariable>
    {
        public override bool Clampable => true;

        protected override ulong ClampValue(ulong value)
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

        public override void Add(ulong other)
        {
            Value += other;
        }

        public override void Subtract(ulong other)
        {
            Value -= other;
        }

        public override void Add(ULongVariable other)
        {
            Value += other.Value;
        }

        public override void Subtract(ULongVariable other)
        {
            Value -= other.Value;
        }
    } 
}