using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ControlsBackButton : MonoBehaviour {
    public GameObject optionsEmpty;
    public GameObject controlsImage;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void BackPressed()
    {
        optionsEmpty.SetActive(true);
        controlsImage.SetActive(false);
    }
}
