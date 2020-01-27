using ScriptableObjectArchitecture.Utility;
using UnityEngine;

namespace ScriptableObjectArchitecture.Variables
{
    [CreateAssetMenu(
        fileName = "CharVariable.asset",
        menuName = SoArchitectureUtility.ADVANCED_VARIABLE_SUBMENU + "char",
        order = SoArchitectureUtility.ASSET_MENU_ORDER_COLLECTIONS + 7)]
    public sealed class CharVariable : BaseVariable<char>
    {
    } 
}