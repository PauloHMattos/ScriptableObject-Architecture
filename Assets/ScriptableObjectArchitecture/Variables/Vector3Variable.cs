using ScriptableObjectArchitecture.Utility;
using UnityEngine;

namespace ScriptableObjectArchitecture.Variables
{
    [CreateAssetMenu(
        fileName = "Vector3Variable.asset",
        menuName = SoArchitectureUtility.VARIABLE_SUBMENU + "Structs/Vector3",
        order = SoArchitectureUtility.ASSET_MENU_ORDER_COLLECTIONS + 11)]
    public sealed class Vector3Variable : NumericVariable<Vector3, Vector3Variable>
    {
        public override void Add(Vector3 other)
        {
            Value += other;
        }

        public override void Subtract(Vector3 other)
        {
            Value -= other;
        }

        public void Cross(Vector3 other)
        {
            Value = Vector3.Cross(Value, other);
        }

        public void Multiply(float other)
        {
            Value *= other;
        }

        public void Scale(Vector3 other)
        {
            _value.Scale(other);
        }

        public void Scale(Vector3Variable other)
        {
            _value.Scale(other);
        }

        public void Scale(Vector2Variable other)
        {
            _value.Scale(other.Value);
        }

        public override void Add(Vector3Variable other)
        {
            Value += other.Value;
        }

        public void Add(Vector2Variable other)
        {
            _value.x += other.Value.x;
            _value.y += other.Value.y;
        }

        public override void Subtract(Vector3Variable other)
        {
            Value -= other.Value;
        }
        public void Subtract(Vector2Variable other)
        {
            _value.x -= other.Value.x;
            _value.y -= other.Value.y;
        }

        public void Cross(Vector3Variable other)
        {
            Value = Vector3.Cross(Value, other);
        }


        public void Cross(Vector2Variable other)
        {
            Value = Vector3.Cross(Value, other.Value);
        }

        public void Multiply(FloatVariable other)
        {
            Value *= other;
        }
    }
}