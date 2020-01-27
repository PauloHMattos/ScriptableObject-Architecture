using ScriptableObjectArchitecture.Utility;
using UnityEngine;

namespace ScriptableObjectArchitecture.Collections
{
    [CreateAssetMenu(
        fileName = "Vector4Collection.asset",
        menuName = SoArchitectureUtility.COLLECTION_SUBMENU + "Structs/Vector4",
        order = SoArchitectureUtility.ASSET_MENU_ORDER_COLLECTIONS + 12)]
    public class Vector4Collection : Collection<Vector4>
    {
    } 
}