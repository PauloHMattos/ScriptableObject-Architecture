using ScriptableObjectArchitecture.Utility;
using UnityEngine;

namespace ScriptableObjectArchitecture.Variables
{
    [CreateAssetMenu(
        fileName = "Vector4Variable.asset",
        menuName = SOArchitecture_Utility.VARIABLE_SUBMENU + "Structs/Vector4",
        order = SOArchitecture_Utility.ASSET_MENU_ORDER_COLLECTIONS + 12)]
    public sealed class Vector4Variable : NumericVariable<Vector4, Vector4Variable>
    {
        public override void Add(Vector4 other)
        {
            Value += other;
        }

        public override void Subtract(Vector4 other)
        {
            Value -= other;
        }

        public override void Add(Vector4Variable other)
        {
            Value += other.Value;
        }

        public override void Subtract(Vector4Variable other)
        {
            Value -= other.Value;
        }
    }
}