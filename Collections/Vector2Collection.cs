using ScriptableObjectArchitecture.Utility;
using UnityEngine;

namespace ScriptableObjectArchitecture.Collections
{
    [CreateAssetMenu(
        fileName = "Vector2Collection.asset",
        menuName = SoArchitectureUtility.COLLECTION_SUBMENU + "Structs/Vector2",
        order = SoArchitectureUtility.ASSET_MENU_ORDER_COLLECTIONS + 10)]
    public class Vector2Collection : Collection<Vector2>
    {
    } 
}