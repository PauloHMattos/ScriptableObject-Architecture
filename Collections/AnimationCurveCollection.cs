using ScriptableObjectArchitecture.Utility;
using UnityEngine;

namespace ScriptableObjectArchitecture.Collections
{
	[CreateAssetMenu(
	    fileName = "AnimationCurveCollection.asset",
	    menuName = SoArchitectureUtility.ADVANCED_VARIABLE_COLLECTION + "AnimationCurve",
	    order = 120)]
	public class AnimationCurveCollection : Collection<AnimationCurve>
	{
	}
}