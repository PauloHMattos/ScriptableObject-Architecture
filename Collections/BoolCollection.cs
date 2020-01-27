using ScriptableObjectArchitecture.Utility;
using UnityEngine;

namespace ScriptableObjectArchitecture.Collections
{
    [CreateAssetMenu(
        fileName = "BoolCollection.asset",
        menuName = SoArchitectureUtility.COLLECTION_SUBMENU + "bool",
        order = SoArchitectureUtility.ASSET_MENU_ORDER_COLLECTIONS + 5)]
    public class BoolCollection : Collection<bool>
    {
    } 
}