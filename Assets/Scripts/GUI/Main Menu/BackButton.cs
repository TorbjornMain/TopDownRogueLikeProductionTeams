using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class BackButton : MonoBehaviour {
    public GameObject menuEmpty;
    public GameObject optionsEmpty;
    public GameObject menuActive;

    // Use this for initialization
    void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void BackPressed()
    {
        menuEmpty.SetActive(true);
        EventSystem.current.GetComponent<EventSystem>().SetSelectedGameObject(menuActive);
        optionsEmpty.SetActive(false);
    }
}
