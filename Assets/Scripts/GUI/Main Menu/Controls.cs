using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Controls : MonoBehaviour {
    public GameObject optionsEmpty;
    public GameObject imagePanel;
    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void ControlsPressed()
    {
        optionsEmpty.SetActive(false);
        imagePanel.SetActive(true);
    }
}
