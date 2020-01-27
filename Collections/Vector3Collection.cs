using ScriptableObjectArchitecture.Utility;
using UnityEngine;

namespace ScriptableObjectArchitecture.Collections
{
    [CreateAssetMenu(
        fileName = "Vector3Collection.asset",
        menuName = SoArchitectureUtility.COLLECTION_SUBMENU + "Structs/Vector3",
        order = SoArchitectureUtility.ASSET_MENU_ORDER_COLLECTIONS + 11)]
    public class Vector3Collection : Collection<Vector3>
    {
    } 
}