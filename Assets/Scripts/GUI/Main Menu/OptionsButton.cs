using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class OptionsButton : MonoBehaviour {

    public GameObject images;
    public GameObject startGamePanel;
    public GameObject optionsPanel;
    public GameObject quitPanel;
    public GameObject controlsPanel;
    public GameObject backPanel;
    public GameObject musicSlider;
    public GameObject soundSlider;

    // Use this for initialization
    void Start () {

    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void OptionsPressed()
    {
        startGamePanel.SetActive(false);
        optionsPanel.SetActive(false);
        quitPanel.SetActive(false);
        controlsPanel.SetActive(true);
        backPanel.SetActive(true);
        musicSlider.SetActive(true);
        soundSlider.SetActive(true);
        images.SetActive(false);
    }
}
