using UnityEngine;
using System.Collections;

public class PauseMenu : MonoBehaviour {
    public GameObject pauseMenu;
    public AudioSource openSound;
    public AudioSource closeSound;

	// Use this for initialization
	void Start () {
    }
	
	// Update is called once per frame
	void Update () {
	    if(Input.GetButtonDown("Cancel"))
        {
            ToggleActive();
        }
	}

    public void ToggleActive()
    {
        if (pauseMenu.activeInHierarchy == true)
        {
            pauseMenu.SetActive(false);
            closeSound.Play();

        }
        else
        {
            pauseMenu.SetActive(true);
            openSound.Play();
        }
    }
}
