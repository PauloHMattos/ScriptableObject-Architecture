using ScriptableObjectArchitecture.Utility;
using UnityEngine;

namespace ScriptableObjectArchitecture.Variables
{
    [CreateAssetMenu(
        fileName = "IntVariable.asset",
        menuName = SOArchitecture_Utility.VARIABLE_SUBMENU + "int",
        order = SOArchitecture_Utility.ASSET_MENU_ORDER_COLLECTIONS + 4)]
    public class IntVariable : NumericVariable<int, IntVariable>
    {
        public override bool Clampable { get { return true; } }
        protected override int ClampValue(int value)
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

        public override void Add(int other)
        {
            Value += other;
        }

        public override void Subtract(int other)
        {
            Value -= other;
        }

        public override void Add(IntVariable other)
        {
            Value += other.Value;
        }

        public override void Subtract(IntVariable other)
        {
            Value -= other.Value;
        }
    } 
}