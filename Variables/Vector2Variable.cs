using UnityEngine;

namespace ScriptableObjectArchitecture
{
    [CreateAssetMenu(
        fileName = "Vector2Variable.asset",
        menuName = SOArchitecture_Utility.VARIABLE_SUBMENU + "Structs/Vector2",
        order = SOArchitecture_Utility.ASSET_MENU_ORDER_COLLECTIONS + 10)]
    public sealed class Vector2Variable : NumericVariable<Vector2, Vector2Variable>
    {
        public override void Add(Vector2 other)
        {
            Value += other;
        }

        public override void Subtract(Vector2 other)
        {
            Value -= other;
        }

        public override void Add(Vector2Variable other)
        {
            Value += other.Value;
        }

        public override void Subtract(Vector2Variable other)
        {
            Value -= other.Value;
        }
    }
}