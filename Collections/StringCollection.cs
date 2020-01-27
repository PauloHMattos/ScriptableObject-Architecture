using ScriptableObjectArchitecture.Utility;
using UnityEngine;

namespace ScriptableObjectArchitecture.Collections
{
    [CreateAssetMenu(
        fileName = "StringCollection.asset",
        menuName = SoArchitectureUtility.COLLECTION_SUBMENU + "string",
        order = SoArchitectureUtility.ASSET_MENU_ORDER_COLLECTIONS + 2)]
    public class StringCollection : Collection<string>
    {
    } 
}