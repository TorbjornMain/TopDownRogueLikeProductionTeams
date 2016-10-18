using UnityEngine;
using System.Collections;


[RequireComponent(typeof(BodySockets))]
public class EnemyAIController : MonoBehaviour {

	private BodySockets bs;

	// Use this for initialization
	void Start () {
		bs = GetComponent<BodySockets> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
