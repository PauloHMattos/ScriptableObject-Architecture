using ScriptableObjectArchitecture.Utility;
using UnityEngine;

namespace ScriptableObjectArchitecture.Variables
{
    [CreateAssetMenu(
        fileName = "UnsignedIntVariable.asset",
        menuName = SoArchitectureUtility.ADVANCED_VARIABLE_SUBMENU + "uint",
        order = SoArchitectureUtility.ASSET_MENU_ORDER_COLLECTIONS + 16)]
    public class UIntVariable : NumericVariable<uint, UIntVariable>
    {
        public override bool Clampable => true;

        protected override uint ClampValue(uint value)
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

        public override void Add(uint other)
        {
            Value += other;
        }

        public override void Subtract(uint other)
        {
            Value -= other;
        }

        public override void Add(UIntVariable other)
        {
            Value += other.Value;
        }

        public override void Subtract(UIntVariable other)
        {
            Value -= other.Value;
        }
    } 
}