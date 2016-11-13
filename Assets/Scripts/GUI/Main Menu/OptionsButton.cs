using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;

public class OptionsButton : MonoBehaviour {

    public GameObject optionsEmpty;
    public GameObject menuEmpty;
    public GameObject controlsButton;

    // Use this for initialization
    void Start () {

    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void OptionsPressed()
    {
        optionsEmpty.SetActive(true);
        EventSystem.current.GetComponent<EventSystem>().SetSelectedGameObject(controlsButton);
        menuEmpty.SetActive(false);
    }
}
