using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MainMenuButtonFader : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void ChangeColour()
    {
        GetComponent<RawImage>().color = new Color32(255,255,255,100);
    }

    public void ChangeColourBack()
    {
        GetComponent<RawImage>().color = new Color32(255, 255, 255, 255);
    }
}
