using ScriptableObjectArchitecture.Utility;
using UnityEngine;

namespace ScriptableObjectArchitecture.Collections
{
	[CreateAssetMenu(
	    fileName = "AudioClipCollection.asset",
	    menuName = SOArchitecture_Utility.ADVANCED_VARIABLE_COLLECTION + "AudioClip",
	    order = 120)]
	public class AudioClipCollection : Collection<AudioClip>
	{
	}
}