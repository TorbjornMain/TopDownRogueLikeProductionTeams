using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SoundFXController : MonoBehaviour {
    float volume;
    Slider soundSlider;

	// Use this for initialization
	void Start () {
        soundSlider = GetComponent<Slider>();
        volume = PlayerPrefs.GetFloat("SoundVolume");
        soundSlider.value = PlayerPrefs.GetFloat("SoundVolume");
        
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void SaveSliderValue()
    {
        volume = soundSlider.value;
        PlayerPrefs.SetFloat("SoundVolume", volume);
    }
}
