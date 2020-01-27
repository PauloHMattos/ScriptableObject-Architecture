using ScriptableObjectArchitecture.Utility;
using UnityEngine;

namespace ScriptableObjectArchitecture.Variables
{
	[CreateAssetMenu(
	    fileName = "KeyCodeVariable.asset",
	    menuName = SoArchitectureUtility.VARIABLE_SUBMENU + "Key Code",
	    order = 120)]
	public class KeyCodeVariable : BaseVariable<KeyCode>
	{
	}
}