using ScriptableObjectArchitecture.Utility;
using UnityEngine;

namespace ScriptableObjectArchitecture.Variables
{
    [CreateAssetMenu(
        fileName = "FloatVariable.asset",
        menuName = SoArchitectureUtility.VARIABLE_SUBMENU + "float",
        order = SoArchitectureUtility.ASSET_MENU_ORDER_COLLECTIONS + 3)]
    public class FloatVariable : NumericVariable<float, FloatVariable>
    {
        public override bool Clampable => true;

        protected override float ClampValue(float value)
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

        public override void Add(float other)
        {
            Value += other;
        }

        public override void Subtract(float other)
        {
            Value -= other;
        }

        public void Multiply(float other)
        {
            Value *= other;
        }

        public override void Add(FloatVariable other)
        {
            Value += other;
        }

        public override void Subtract(FloatVariable other)
        {
            Value -= other;
        }

        public void Multiply(FloatVariable other)
        {
            Value *= other;
        }
    }
}