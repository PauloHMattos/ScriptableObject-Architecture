using ScriptableObjectArchitecture.Utility;
using UnityEngine;

namespace ScriptableObjectArchitecture.Collections
{
    [CreateAssetMenu(
        fileName = "UIntCollection.asset",
        menuName = SoArchitectureUtility.ADVANCED_VARIABLE_COLLECTION + "uint",
        order = SoArchitectureUtility.ASSET_MENU_ORDER_COLLECTIONS + 16)]
    public class UIntCollection : Collection<uint>
    {
    } 
}