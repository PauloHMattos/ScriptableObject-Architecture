using ScriptableObjectArchitecture.Utility;
using UnityEngine;

namespace ScriptableObjectArchitecture.Variables
{
	[CreateAssetMenu(
	    fileName = "ColorVariable.asset",
	    menuName = SoArchitectureUtility.VARIABLE_SUBMENU + "Structs/Color",
	    order = 120)]
	public class ColorVariable : BaseVariable<Color>
	{
	}
}