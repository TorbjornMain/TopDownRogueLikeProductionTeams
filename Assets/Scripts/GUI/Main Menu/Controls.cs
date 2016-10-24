using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Controls : MonoBehaviour {
    public RawImage logo;
    public Texture controls;
    public GameObject controlsPanel;
    public GameObject backPanel;
    public GameObject controlsBackPanel;
    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void ControlsPressed()
    {
        logo.GetComponent<RawImage>().texture = controls;
        controlsPanel.SetActive(false);
        backPanel.SetActive(false);
        controlsBackPanel.SetActive(true);
    }
}
