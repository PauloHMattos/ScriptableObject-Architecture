using ScriptableObjectArchitecture;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetAudioSource : MonoBehaviour
{
    public AudioClipVariable[] AudioClips;
    public AudioSource AudioSource;
    // Start is called before the first frame update
    void OnEnable()
    {
        for (int i = 0; i < AudioClips.Length; i++)
        {
            AudioClips[i].Source = AudioSource;
        }
    }

    // Update is called once per frame
    void OnDisable()
    {
        for (int i = 0; i < AudioClips.Length; i++)
        {
            AudioClips[i].Source = null;
        }
    }
}
