using ScriptableObjectArchitecture.Utility;
using UnityEngine;

namespace ScriptableObjectArchitecture.Collections
{
    [CreateAssetMenu(
        fileName = "QuaternionCollection.asset",
        menuName = SoArchitectureUtility.COLLECTION_SUBMENU + "Structs/Quaternion",
        order = SoArchitectureUtility.ASSET_MENU_ORDER_COLLECTIONS + 13)]
    public class QuaternionCollection : Collection<Quaternion>
    {
    } 
}