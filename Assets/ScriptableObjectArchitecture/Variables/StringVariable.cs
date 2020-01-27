using ScriptableObjectArchitecture.Utility;
using UnityEngine;

namespace ScriptableObjectArchitecture.Variables
{
    [CreateAssetMenu(
        fileName = "StringVariable.asset",
        menuName = SoArchitectureUtility.VARIABLE_SUBMENU + "string",
        order = SoArchitectureUtility.ASSET_MENU_ORDER_COLLECTIONS + 2)]
    public sealed class StringVariable : BaseVariable<string>
    {
        public override void Awake()
        {
            base.Awake();
            // Evita que o valor seja inicializado como nulo
            Value = "";
        }
    }
}