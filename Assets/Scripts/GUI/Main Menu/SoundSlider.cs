using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SoundSlider : MonoBehaviour
{
    float volume = 1f;
    Slider soundSlider;
    // Use this for initialization
    void Start()
    {
        soundSlider = GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SaveSliderValue()
    {
        volume = soundSlider.value;
        PlayerPrefs.SetFloat("SoundSliderVolumeLevel", volume);
    }
}
