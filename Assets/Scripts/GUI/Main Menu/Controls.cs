using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;

public class Controls : MonoBehaviour {
    public GameObject optionsEmpty;
    public GameObject imagePanel;
    public GameObject backActive;
    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void ControlsPressed()
    {
        optionsEmpty.SetActive(false);
        EventSystem.current.GetComponent<EventSystem>().SetSelectedGameObject(backActive);
        imagePanel.SetActive(true);
    }
}
