using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Controls : MonoBehaviour {
    public GameObject controlsPanel;
    public GameObject backPanel;
    public GameObject controlsBackPanel;
    public GameObject imagePanel;
    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void ControlsPressed()
    {
        controlsPanel.SetActive(false);
        backPanel.SetActive(false);
        controlsBackPanel.SetActive(true);
        imagePanel.SetActive(true);
    }
}
