using ScriptableObjectArchitecture.Utility;
using UnityEngine;

namespace ScriptableObjectArchitecture.Collections
{
	[CreateAssetMenu(
	    fileName = "ColorCollection.asset",
	    menuName = SoArchitectureUtility.COLLECTION_SUBMENU + "Structs/Color",
	    order = 120)]
	public class ColorCollection : Collection<Color>
	{
	}
}