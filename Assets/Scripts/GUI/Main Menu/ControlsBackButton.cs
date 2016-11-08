using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ControlsBackButton : MonoBehaviour {
    public GameObject controlsPanel;
    public GameObject backPanel;
    public GameObject controlsBackPanel;
    public GameObject controlsImage;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void BackPressed()
    {
        controlsPanel.SetActive(true);
        backPanel.SetActive(true);
        controlsBackPanel.SetActive(false);
    }
}
