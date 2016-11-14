using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class PauseMenu : MonoBehaviour {
    public GameObject pauseMenu;
    public AudioSource openSound;
    public AudioSource closeSound;
    public GameObject resumeButton;

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
            Time.timeScale = 1;
            pauseMenu.SetActive(false);
            closeSound.Play();
            

        }
        else
        {
            pauseMenu.SetActive(true);
            EventSystem.current.GetComponent<EventSystem>().SetSelectedGameObject(resumeButton);
            openSound.Play();
            Time.timeScale = 0;
        }
    }
}
