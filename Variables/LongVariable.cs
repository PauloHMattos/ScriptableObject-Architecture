using ScriptableObjectArchitecture.Utility;
using UnityEngine;

namespace ScriptableObjectArchitecture.Variables
{
    [CreateAssetMenu(
        fileName = "LongVariable.asset",
        menuName = SOArchitecture_Utility.ADVANCED_VARIABLE_SUBMENU + "long",
        order = SOArchitecture_Utility.ASSET_MENU_ORDER_COLLECTIONS + 9)]
    public class LongVariable : NumericVariable<long, LongVariable>
    {
        public override bool Clampable { get { return true; } }
        protected override long ClampValue(long value)
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

        public override void Add(long other)
        {
            Value += other;
        }

        public override void Subtract(long other)
        {
            Value -= other;
        }

        public override void Add(LongVariable other)
        {
            Value += other.Value;
        }

        public override void Subtract(LongVariable other)
        {
            Value -= other.Value;
        }
    } 
}