using ScriptableObjectArchitecture.Utility;
using UnityEngine;

namespace ScriptableObjectArchitecture.Variables
{
    [CreateAssetMenu(
        fileName = "DoubleVariable.asset",
        menuName = SOArchitecture_Utility.ADVANCED_VARIABLE_SUBMENU + "double",
        order = SOArchitecture_Utility.ASSET_MENU_ORDER_COLLECTIONS + 8)]
    public class DoubleVariable : NumericVariable<double, DoubleVariable>
    {
        public override bool Clampable { get { return true; } }
        protected override double ClampValue(double value)
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

        public override void Add(double other)
        {
            Value += other;
        }

        public override void Subtract(double other)
        {
            Value -= other;
        }

        public override void Add(DoubleVariable other)
        {
            Value += other.Value;
        }

        public override void Subtract(DoubleVariable other)
        {
            Value -= other.Value;
        }
    }
}