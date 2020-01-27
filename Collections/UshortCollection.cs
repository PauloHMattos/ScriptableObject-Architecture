using ScriptableObjectArchitecture.Utility;
using UnityEngine;

namespace ScriptableObjectArchitecture.Collections
{
    [CreateAssetMenu(
        fileName = "UShortCollection.asset",
        menuName = SoArchitectureUtility.ADVANCED_VARIABLE_COLLECTION + "ushort",
        order = SoArchitectureUtility.ASSET_MENU_ORDER_COLLECTIONS + 18)]
    public class UShortCollection : Collection<ushort>
    {
    } 
}