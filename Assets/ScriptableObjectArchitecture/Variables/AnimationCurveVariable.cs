using ScriptableObjectArchitecture.Utility;
using UnityEngine;

namespace ScriptableObjectArchitecture.Variables
{
	[CreateAssetMenu(
	    fileName = "AnimationCurveVariable.asset",
	    menuName = SOArchitecture_Utility.ADVANCED_VARIABLE_SUBMENU + "AnimationCurve",
	    order = 120)]
	public class AnimationCurveVariable : BaseVariable<AnimationCurve>
	{
	}
}