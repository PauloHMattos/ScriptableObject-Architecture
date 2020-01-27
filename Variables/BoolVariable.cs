using ScriptableObjectArchitecture.Utility;
using UnityEngine;

namespace ScriptableObjectArchitecture.Variables
{
    [CreateAssetMenu(
        fileName = "BoolVariable.asset",
        menuName = SoArchitectureUtility.VARIABLE_SUBMENU + "bool",
        order = SoArchitectureUtility.ASSET_MENU_ORDER_COLLECTIONS + 5)]
    public sealed class BoolVariable : BaseVariable<bool>
    {
    } 
}