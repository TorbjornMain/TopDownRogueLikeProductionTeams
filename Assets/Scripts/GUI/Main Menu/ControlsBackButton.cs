using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;

public class ControlsBackButton : MonoBehaviour {
    public GameObject optionsEmpty;
    public GameObject controlsImage;
    public GameObject optionsActive;


    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void BackPressed()
    {
        optionsEmpty.SetActive(true);
        EventSystem.current.GetComponent<EventSystem>().SetSelectedGameObject(optionsActive);
        controlsImage.SetActive(false);
    }
}
