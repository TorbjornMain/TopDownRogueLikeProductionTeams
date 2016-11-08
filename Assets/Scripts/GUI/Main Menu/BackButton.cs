using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BackButton : MonoBehaviour {
    public GameObject startGamePanel;
    public GameObject optionsPanel;
    public GameObject quitPanel;
    public GameObject controlsPanel;
    public GameObject backPanel;
    public GameObject musicSlider;
    public GameObject soundSlider;
    public GameObject imageEmpty;

    // Use this for initialization
    void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void BackPressed()
    {
        startGamePanel.SetActive(true);
        optionsPanel.SetActive(true);
        quitPanel.SetActive(true);
        imageEmpty.SetActive(true);
        controlsPanel.SetActive(false);
        backPanel.SetActive(false);
        musicSlider.SetActive(false);
        soundSlider.SetActive(false);
    }
}
