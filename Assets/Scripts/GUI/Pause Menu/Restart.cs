using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Restart : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void RestartRun()
    {
        SceneManager.LoadSceneAsync("RileyArena", LoadSceneMode.Single);
    }
}
