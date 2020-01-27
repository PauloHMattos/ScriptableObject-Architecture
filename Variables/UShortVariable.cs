using ScriptableObjectArchitecture.Utility;
using UnityEngine;

namespace ScriptableObjectArchitecture.Variables
{
    [CreateAssetMenu(
        fileName = "UnsignedShortVariable.asset",
        menuName = SoArchitectureUtility.ADVANCED_VARIABLE_SUBMENU + "ushort",
        order = SoArchitectureUtility.ASSET_MENU_ORDER_COLLECTIONS + 18)]
    public class UShortVariable : NumericVariable<ushort, UShortVariable>
    {
        public override bool Clampable => true;

        protected override ushort ClampValue(ushort value)
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

        public override void Add(ushort other)
        {
            Value += other;
        }

        public override void Subtract(ushort other)
        {
            Value -= other;
        }

        public override void Add(UShortVariable other)
        {
            Value += other.Value;
        }

        public override void Subtract(UShortVariable other)
        {
            Value -= other.Value;
        }
    } 
}