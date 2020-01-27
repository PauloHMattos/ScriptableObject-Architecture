using ScriptableObjectArchitecture.Utility;
using UnityEngine;

namespace ScriptableObjectArchitecture.Collections
{
    [CreateAssetMenu(
        fileName = "LongCollection.asset",
        menuName = SoArchitectureUtility.ADVANCED_VARIABLE_COLLECTION + "long",
        order = SoArchitectureUtility.ASSET_MENU_ORDER_COLLECTIONS + 9)]
    public class LongCollection : Collection<long>
    {
    } 
}