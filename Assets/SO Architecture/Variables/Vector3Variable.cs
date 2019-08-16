using UnityEngine;

namespace ScriptableObjectArchitecture
{
    [CreateAssetMenu(
        fileName = "Vector3Variable.asset",
        menuName = SOArchitecture_Utility.VARIABLE_SUBMENU + "Structs/Vector3",
        order = SOArchitecture_Utility.ASSET_MENU_ORDER_COLLECTIONS + 11)]
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

        public override void Add(Vector3Variable other)
        {
            Value += other.Value;
        }

        public override void Subtract(Vector3Variable other)
        {
            Value -= other.Value;
        }
    }
}