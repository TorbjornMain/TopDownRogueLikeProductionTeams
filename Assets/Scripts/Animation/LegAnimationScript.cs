using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Transport))]
[RequireComponent(typeof(Animator))]
public class LegAnimationScript : MonoBehaviour {

	private Transport t;
	private Animator a;

	// Use this for initialization
	void Start () {
		t = GetComponent<Transport> ();
		a = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
		a.SetFloat ("Speed", t.transportSpeed); 
	}
}
