using ScriptableObjectArchitecture.Utility;
using UnityEngine;

namespace ScriptableObjectArchitecture.Variables
{
    [CreateAssetMenu(
        fileName = "GameObjectVariable.asset",
        menuName = SoArchitectureUtility.VARIABLE_SUBMENU + "GameObject",
        order = SoArchitectureUtility.ASSET_MENU_ORDER_COLLECTIONS + 0)]
    public sealed class GameObjectVariable : BaseVariable<GameObject>
    {
    } 
}