using ScriptableObjectArchitecture.Utility;
using UnityEngine;

namespace ScriptableObjectArchitecture.Variables
{
	[CreateAssetMenu(
	    fileName = "AudioClipVariable.asset",
	    menuName = SoArchitectureUtility.ADVANCED_VARIABLE_SUBMENU + "AudioClip",
	    order = 120)]
	public class AudioClipVariable : BaseVariable<AudioClip>
	{
	}
}