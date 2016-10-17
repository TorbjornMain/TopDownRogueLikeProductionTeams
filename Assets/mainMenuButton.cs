using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class mainMenuButton : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void changeColour()
    {
        GetComponent<RawImage>().color = new Color32(255,255,255,100);
    }
}
