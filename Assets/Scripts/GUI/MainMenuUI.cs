using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MainMenuUI : MonoBehaviour {

    //public GameObject mainMenu;
    //public Texture mainMenu;
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
            //GetComponent<RawImage>().texture = mainMenu;
            logoHolder.SetActive(false);
            imageSwapped = true;
            mainMenuHolder.SetActive(true);
        }
    }
}
