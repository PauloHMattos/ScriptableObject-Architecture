using ScriptableObjectArchitecture.Utility;
using UnityEngine;

namespace ScriptableObjectArchitecture.Collections
{
	[CreateAssetMenu(
	    fileName = "AudioClipCollection.asset",
	    menuName = SoArchitectureUtility.ADVANCED_VARIABLE_COLLECTION + "AudioClip",
	    order = 120)]
	public class AudioClipCollection : Collection<AudioClip>
	{
	}
}