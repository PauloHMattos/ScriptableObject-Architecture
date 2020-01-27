using ScriptableObjectArchitecture.Utility;
using UnityEngine;

namespace ScriptableObjectArchitecture.Variables
{
    [CreateAssetMenu(
        fileName = "ObjectVariable.asset",
        menuName = SoArchitectureUtility.VARIABLE_SUBMENU + "Object",
        order = SoArchitectureUtility.ASSET_MENU_ORDER_COLLECTIONS + 1)]
    public class ObjectVariable : BaseVariable<Object>
    {
    } 
}