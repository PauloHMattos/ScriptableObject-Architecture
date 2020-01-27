using ScriptableObjectArchitecture.Utility;
using UnityEngine;

namespace ScriptableObjectArchitecture.Collections
{
    [CreateAssetMenu(
        fileName = "ShortCollection.asset",
        menuName = SoArchitectureUtility.ADVANCED_VARIABLE_COLLECTION + "short",
        order = SoArchitectureUtility.ASSET_MENU_ORDER_COLLECTIONS + 14)]
    public class ShortCollection : Collection<short>
    {
    } 
}