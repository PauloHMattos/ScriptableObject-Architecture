using ScriptableObjectArchitecture.Utility;
using UnityEngine;

namespace ScriptableObjectArchitecture.Variables
{
    [CreateAssetMenu(
        fileName = "Vector2Variable.asset",
        menuName = SoArchitectureUtility.VARIABLE_SUBMENU + "Structs/Vector2",
        order = SoArchitectureUtility.ASSET_MENU_ORDER_COLLECTIONS + 10)]
    public class Vector2Variable : NumericVariable<Vector2, Vector2Variable>
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