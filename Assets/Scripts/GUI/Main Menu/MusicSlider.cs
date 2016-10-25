using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MusicSlider : MonoBehaviour {
    float volume = 1f;
    Slider musicSlider;
	// Use this for initialization
	void Start () {
        musicSlider = GetComponent<Slider>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void SaveSliderValue()
    {
        volume = musicSlider.value;
        PlayerPrefs.SetFloat("SliderVolumeLevel", volume);
    }
}
