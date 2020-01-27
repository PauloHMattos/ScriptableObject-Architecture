using ScriptableObjectArchitecture.Utility;
using UnityEngine;

namespace ScriptableObjectArchitecture.Collections
{
    [CreateAssetMenu(
        fileName = "IntCollection.asset",
        menuName = SoArchitectureUtility.COLLECTION_SUBMENU + "int",
        order = SoArchitectureUtility.ASSET_MENU_ORDER_COLLECTIONS + 4)]
    public class IntCollection : Collection<int>
    {
    } 
}