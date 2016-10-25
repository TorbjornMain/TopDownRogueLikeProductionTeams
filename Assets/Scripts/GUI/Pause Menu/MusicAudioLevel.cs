using UnityEngine;
using System.Collections;

public class MusicAudioLevel : MonoBehaviour {
    AudioSource bGM;
	// Use this for initialization
	void Start () {
        bGM = GetComponent<AudioSource>();
        bGM.volume = PlayerPrefs.GetFloat("SliderVolumeLevel",bGM.volume);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
