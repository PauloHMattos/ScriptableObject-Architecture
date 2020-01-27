using ScriptableObjectArchitecture.Utility;
using UnityEngine;

namespace ScriptableObjectArchitecture.Variables
{
    [CreateAssetMenu(
        fileName = "ShortVariable.asset",
        menuName = SOArchitecture_Utility.ADVANCED_VARIABLE_SUBMENU + "short",
        order = SOArchitecture_Utility.ASSET_MENU_ORDER_COLLECTIONS + 14)]
    public class ShortVariable : NumericVariable<short, ShortVariable>
    {
        public override bool Clampable { get { return true; } }
        protected override short ClampValue(short value)
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

        public override void Add(short other)
        {
            Value += other;
        }

        public override void Subtract(short other)
        {
            Value -= other;
        }

        public override void Add(ShortVariable other)
        {
            Value += other.Value;
        }

        public override void Subtract(ShortVariable other)
        {
            Value -= other.Value;
        }
    } 
}