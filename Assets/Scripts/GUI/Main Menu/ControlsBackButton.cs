using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ControlsBackButton : MonoBehaviour {
    public RawImage logo;
    public Texture options;
    public GameObject controlsPanel;
    public GameObject backPanel;
    public GameObject controlsBackPanel;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void BackPressed()
    {
        logo.GetComponent<RawImage>().texture = options;
        controlsPanel.SetActive(true);
        backPanel.SetActive(true);
        controlsBackPanel.SetActive(false);
    }
}
