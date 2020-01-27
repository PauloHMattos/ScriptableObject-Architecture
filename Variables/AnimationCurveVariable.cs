using ScriptableObjectArchitecture.Utility;
using UnityEngine;

namespace ScriptableObjectArchitecture.Variables
{
	[CreateAssetMenu(
	    fileName = "AnimationCurveVariable.asset",
	    menuName = SoArchitectureUtility.ADVANCED_VARIABLE_SUBMENU + "AnimationCurve",
	    order = 120)]
	public class AnimationCurveVariable : BaseVariable<AnimationCurve>
	{
	}
}