using UnityEngine;

namespace ScriptableObjectArchitecture
{
	[CreateAssetMenu(
	    fileName = "AudioClipVariable.asset",
	    menuName = SOArchitecture_Utility.ADVANCED_VARIABLE_SUBMENU + "AudioClip",
	    order = 120)]
	public class AudioClipVariable : BaseVariable<AudioClip>
	{
        public AudioSource Source;

        public void Play()
        {
            if (Source != null)
            {
                Source.Play();
            }
        }

        public void Stop()
        {
            if (Source != null)
            {
                Source.Stop();
            }
        }
    }
}