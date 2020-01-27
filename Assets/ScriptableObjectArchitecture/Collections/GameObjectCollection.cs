using ScriptableObjectArchitecture.Utility;
using UnityEngine;

namespace ScriptableObjectArchitecture.Collections
{
    [CreateAssetMenu(
        fileName = "GameObjectCollection.asset",
        menuName = SoArchitectureUtility.COLLECTION_SUBMENU + "GameObject",
        order = SoArchitectureUtility.ASSET_MENU_ORDER_COLLECTIONS + 0)]
    public class GameObjectCollection : Collection<GameObject>
    {
    } 
}
