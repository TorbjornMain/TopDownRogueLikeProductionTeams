using UnityEngine;
using System.Collections;

public class SoundFXAudioLevel : MonoBehaviour {

    AudioSource sound;
    // Use this for initialization
    void Start()
    {
        sound = GetComponent<AudioSource>();
        sound.volume = PlayerPrefs.GetFloat("SoundSliderVolumeLevel", sound.volume);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
