using ScriptableObjectArchitecture.Utility;
using UnityEngine;

namespace ScriptableObjectArchitecture.Collections
{
    [CreateAssetMenu(
        fileName = "DoubleCollection.asset",
        menuName = SoArchitectureUtility.ADVANCED_VARIABLE_COLLECTION + "double",
        order = SoArchitectureUtility.ASSET_MENU_ORDER_COLLECTIONS + 8)]
    public class DoubleCollection : Collection<double>
    {
    } 
}