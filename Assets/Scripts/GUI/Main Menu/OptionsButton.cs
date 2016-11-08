using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class OptionsButton : MonoBehaviour {

    public GameObject optionsEmpty;
    public GameObject menuEmpty;


    // Use this for initialization
    void Start () {

    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void OptionsPressed()
    {
        optionsEmpty.SetActive(true);
        menuEmpty.SetActive(false);
    }
}
