using UnityEngine;
using System.Collections;

public class MusicSlider : MonoBehaviour {
    float volume = 1.0f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void SaveSliderValue()
    {
        PlayerPrefs.SetFloat("SliderVolumeLevel", volume);
    }
}
