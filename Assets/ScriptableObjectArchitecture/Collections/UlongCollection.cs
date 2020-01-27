using ScriptableObjectArchitecture.Utility;
using UnityEngine;

namespace ScriptableObjectArchitecture.Collections
{
    [CreateAssetMenu(
        fileName = "ULongCollection.asset",
        menuName = SoArchitectureUtility.ADVANCED_VARIABLE_COLLECTION + "ulong",
        order = SoArchitectureUtility.ASSET_MENU_ORDER_COLLECTIONS + 17)]
    public class ULongCollection : Collection<ulong>
    {
    } 
}