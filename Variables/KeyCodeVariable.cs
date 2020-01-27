using ScriptableObjectArchitecture.Utility;
using UnityEngine;

namespace ScriptableObjectArchitecture.Variables
{
	[CreateAssetMenu(
	    fileName = "KeyCodeVariable.asset",
	    menuName = SOArchitecture_Utility.VARIABLE_SUBMENU + "Key Code",
	    order = 120)]
	public class KeyCodeVariable : BaseVariable<KeyCode>
	{
	}
}