using ScriptableObjectArchitecture.Utility;
using UnityEngine;

namespace ScriptableObjectArchitecture.Collections
{
    [CreateAssetMenu(
        fileName = "FloatCollection.asset",
        menuName = SoArchitectureUtility.COLLECTION_SUBMENU + "float",
        order = SoArchitectureUtility.ASSET_MENU_ORDER_COLLECTIONS + 3)]
    public class FloatCollection : Collection<float>
    {
    } 
}