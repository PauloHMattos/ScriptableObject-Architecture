using ScriptableObjectArchitecture.Utility;
using UnityEngine;

namespace ScriptableObjectArchitecture.Variables
{
	[CreateAssetMenu(
	    fileName = "Color32Variable.asset",
	    menuName = SoArchitectureUtility.VARIABLE_SUBMENU + "Structs/Color32",
	    order = 120)]
	public class Color32Variable : BaseVariable<Color32>
	{
	}
}