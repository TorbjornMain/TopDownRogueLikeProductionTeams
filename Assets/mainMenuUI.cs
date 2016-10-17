using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class mainMenuUI : MonoBehaviour {

    //public GameObject mainMenu;
    public Texture mainMenu;
    private bool imageSwapped = false;
    public GameObject startGamePanel;
    public GameObject optionsPanel;
    public GameObject quitPanel;


	// Use this for initialization
	void Start () {
	}

    // Update is called once per frame
    void Update()
    {
        if (imageSwapped == false && Input.anyKey)
        {
            GetComponent<RawImage>().texture = mainMenu;
            startGamePanel.SetActive(true);
            optionsPanel.SetActive(true);
            quitPanel.SetActive(true);
        }
    }
}
