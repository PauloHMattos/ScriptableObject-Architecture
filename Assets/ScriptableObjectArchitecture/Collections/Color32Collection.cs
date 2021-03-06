using ScriptableObjectArchitecture.Utility;
using UnityEngine;

namespace ScriptableObjectArchitecture.Collections
{
	[CreateAssetMenu(
	    fileName = "Color32Collection.asset",
	    menuName = SoArchitectureUtility.COLLECTION_SUBMENU + "Structs/Color32",
	    order = 120)]
	public class Color32Collection : Collection<Color32>
	{
	}
}