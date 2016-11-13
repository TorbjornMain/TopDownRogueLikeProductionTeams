using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;

public class MainMenuUI : MonoBehaviour {

    public GameObject playActive;
    private bool imageSwapped = false;
    public GameObject mainMenuHolder;
    public GameObject logoHolder;


	// Use this for initialization
	void Start () {
	}

    // Update is called once per frame
    void Update()
    {
        if (imageSwapped == false && Input.anyKey)
        {
            EventSystem.current.GetComponent<EventSystem>().SetSelectedGameObject(playActive);
            logoHolder.SetActive(false);
            imageSwapped = true;
            mainMenuHolder.SetActive(true);
        }
    }
}
