using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class StartRoomWarp : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "player")
        {
            print("player walked through portal");
            SceneManager.LoadSceneAsync("RileyArena", LoadSceneMode.Single);
        }
    }
}
