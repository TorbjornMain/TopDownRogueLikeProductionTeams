using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MusicController : MonoBehaviour {
    public AudioSource bGM;
    Slider musicSlider;
    float volume;
    // Use this for initialization
    void Start () {
        musicSlider = GetComponent<Slider>();
        bGM.volume = PlayerPrefs.GetFloat("SliderVolumeLevel", bGM.volume);
        print(bGM.volume);
        GetComponent<Slider>().value = bGM.volume;
        
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
