using ScriptableObjectArchitecture.Utility;
using UnityEngine;

namespace ScriptableObjectArchitecture.Collections
{
    [CreateAssetMenu(
        fileName = "CharCollection.asset",
        menuName = SoArchitectureUtility.ADVANCED_VARIABLE_COLLECTION + "char",
        order = SoArchitectureUtility.ASSET_MENU_ORDER_COLLECTIONS + 7)]
    public class CharCollection : Collection<char>
    {
    } 
}