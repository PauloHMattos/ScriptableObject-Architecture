using ScriptableObjectArchitecture.Utility;
using UnityEngine;

namespace ScriptableObjectArchitecture.Collections
{
    [CreateAssetMenu(
        fileName = "SByteCollection.asset",
        menuName = SoArchitectureUtility.ADVANCED_VARIABLE_COLLECTION + "sbyte",
        order = SoArchitectureUtility.ASSET_MENU_ORDER_COLLECTIONS + 15)]
    public class SByteCollection : Collection<sbyte>
    {
    } 
}