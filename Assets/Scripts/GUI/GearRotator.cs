using UnityEngine;
using System.Collections;

public class GearRotator : MonoBehaviour {
    public int speed; 
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        transform.Rotate(new Vector3(0, 0, speed) * Time.deltaTime);
    }
}
