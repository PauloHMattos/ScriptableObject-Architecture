using ScriptableObjectArchitecture.Utility;
using UnityEngine;

namespace ScriptableObjectArchitecture.Variables
{
    [CreateAssetMenu(
        fileName = "QuaternionVariable.asset",
        menuName = SoArchitectureUtility.VARIABLE_SUBMENU + "Structs/Quaternion",
        order = SoArchitectureUtility.ASSET_MENU_ORDER_COLLECTIONS + 13)]
    public sealed class QuaternionVariable : BaseVariable<Quaternion>
    {
    } 
}