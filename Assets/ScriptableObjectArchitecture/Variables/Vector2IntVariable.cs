using ScriptableObjectArchitecture.Utility;
using UnityEngine;

namespace ScriptableObjectArchitecture.Variables
{
    [CreateAssetMenu(
        fileName = "Vector2IntVariable.asset",
        menuName = SoArchitectureUtility.VARIABLE_SUBMENU + "Structs/Vector2Int",
        order = SoArchitectureUtility.ASSET_MENU_ORDER_COLLECTIONS + 13)]
    public sealed class Vector2IntVariable : NumericVariable<Vector2Int, Vector2IntVariable>
    {
        public override void Add(Vector2Int other)
        {
            Value += other;
        }

        public override void Subtract(Vector2Int other)
        {
            Value -= other;
        }

        public override void Add(Vector2IntVariable other)
        {
            Value += other.Value;
        }

        public override void Subtract(Vector2IntVariable other)
        {
            Value -= other.Value;
        }
    }
}