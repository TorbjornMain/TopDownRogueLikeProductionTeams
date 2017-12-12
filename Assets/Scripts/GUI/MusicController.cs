using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MusicController : MonoBehaviour {
    Slider musicSlider;
    float volume = 0;
    // Use this for initialization
    void Start () {
        musicSlider = GetComponent<Slider>();
        musicSlider.value = PlayerPrefs.GetFloat("MusicVolume", 1);
	}

    public void SaveSliderValue()
    {
        volume = musicSlider.value;
        print(volume);
        PlayerPrefs.SetFloat("MusicVolume", volume);
    }
}
