using ScriptableObjectArchitecture.Utility;
using UnityEngine;

namespace ScriptableObjectArchitecture.Collections
{
    [CreateAssetMenu(
        fileName = "ByteCollection.asset",
        menuName = SoArchitectureUtility.ADVANCED_VARIABLE_COLLECTION + "byte",
        order = SoArchitectureUtility.ASSET_MENU_ORDER_COLLECTIONS + 6)]
    public class ByteCollection : Collection<byte>
    {
    } 
}