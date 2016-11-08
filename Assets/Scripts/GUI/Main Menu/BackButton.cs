using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BackButton : MonoBehaviour {
    public GameObject menuEmpty;
    public GameObject optionsEmpty;

    // Use this for initialization
    void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void BackPressed()
    {
        menuEmpty.SetActive(true);
        optionsEmpty.SetActive(false);
    }
}
