using ScriptableObjectArchitecture.Utility;
using UnityEngine;

namespace ScriptableObjectArchitecture.Collections
{
    [CreateAssetMenu(
        fileName = "ObjectCollection.asset",
        menuName = SoArchitectureUtility.COLLECTION_SUBMENU + "Object",
        order = SoArchitectureUtility.ASSET_MENU_ORDER_COLLECTIONS + 1)]
    public class ObjectCollection : Collection<Object>
    {
    } 
}