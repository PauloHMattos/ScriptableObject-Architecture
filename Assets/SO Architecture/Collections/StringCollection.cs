using UnityEngine;

namespace ScriptableObjectArchitecture
{
    [CreateAssetMenu(
        fileName = "StringCollection.asset",
        menuName = SOArchitecture_Utility.COLLECTION_SUBMENU + "string",
        order = SOArchitecture_Utility.ASSET_MENU_ORDER_COLLECTIONS + 2)]
    public class StringCollection : Collection<string>
    {
        [Range(0, 50)]
        public int a;
        [SerializeField]
        private string b;
        [SerializeField, HideInInspector]
        private float c;
        [SerializeField]
        private bool d;
    } 
}