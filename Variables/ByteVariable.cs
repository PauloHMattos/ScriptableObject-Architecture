using ScriptableObjectArchitecture.Utility;
using UnityEngine;

namespace ScriptableObjectArchitecture.Variables
{
    [CreateAssetMenu(
        fileName = "ByteVariable.asset",
        menuName = SoArchitectureUtility.ADVANCED_VARIABLE_SUBMENU + "byte",
        order = SoArchitectureUtility.ASSET_MENU_ORDER_COLLECTIONS + 6)]
    public class ByteVariable : NumericVariable<byte, ByteVariable>
    {
        public override bool Clampable => true;

        protected override byte ClampValue(byte value)
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

        public override void Add(byte other)
        {
            Value += other;
        }

        public override void Subtract(byte other)
        {
            Value -= other;
        }

        public override void Add(ByteVariable other)
        {
            Value += other.Value;
        }

        public override void Subtract(ByteVariable other)
        {
            Value -= other.Value;
        }
    } 
}