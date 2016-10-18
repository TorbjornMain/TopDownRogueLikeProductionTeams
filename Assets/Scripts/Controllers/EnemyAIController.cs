using UnityEngine;
using System.Collections;


[RequireComponent(typeof(BodySockets))]
[RequireComponent(typeof(SpawnableObject))]
public class EnemyAIController : MonoBehaviour {

	private BodySockets bs;
	private SpawnableObject sp;
	// Use this for initialization
	void Start () {
		bs = GetComponent<BodySockets> ();
		sp = GetComponent<SpawnableObject> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
