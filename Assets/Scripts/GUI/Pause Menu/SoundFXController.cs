using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SoundFXController : MonoBehaviour {
    public AudioSource back;
    public AudioSource confirm;
    public AudioSource close;
    public AudioSource open;
    float volume;
    Slider soundSlider;

	// Use this for initialization
	void Start () {
        soundSlider = GetComponent<Slider>();
        volume = PlayerPrefs.GetFloat("SoundSliderVolumeLevel");
        back.volume = PlayerPrefs.GetFloat("SoundSliderVolumeLevel", back.volume);
        confirm.volume = PlayerPrefs.GetFloat("SoundSliderVolumeLevel", confirm.volume);
        close.volume = PlayerPrefs.GetFloat("SoundSliderVolumeLevel", close.volume);
        open.volume = PlayerPrefs.GetFloat("SoundSliderVolumeLevel", open.volume);
        print(back.volume);
        GetComponent<Slider>().value = PlayerPrefs.GetFloat("SoundSliderVolumeLevel");
        
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void SaveSliderValue()
    {
        volume = soundSlider.value;
        PlayerPrefs.SetFloat("SoundSliderVolumeLevel", volume);
    }
}
