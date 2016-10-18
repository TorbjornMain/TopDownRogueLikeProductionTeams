using UnityEngine;
using System.Collections;


public enum EnemyAIState
{
	Wander,
	Flee,
	Pursue
}


[RequireComponent(typeof(BodySockets))]
[RequireComponent(typeof(SpawnableObject))]
[RequireComponent(typeof(DamageableItem))]
public class EnemyAIController : MonoBehaviour {

	public float aggroRadius = 10.0f;
	public float fleeThreshold = 0.2f;
	public float aggroThreshold = 0.2f;
	public float aggroGainRate = 1;
	public float aggroDecayRate = 1;

	private float aggro = 0.0f;
	private EnemyAIState state = EnemyAIState.Wander;
	private BodySockets bs;
	private SpawnableObject sp;
	// Use this for initialization
	void Start () {
		bs = GetComponent<BodySockets> ();
		sp = GetComponent<SpawnableObject> ();
	}
	
	// Update is called once per frame
	void Update () {
		float playerDistance = (sp.lm.playerInstance.transform.position - transform.position).magnitude;
		if (playerDistance < aggroRadius) {
			aggro += aggroGainRate * Time.deltaTime;
		} else {
			aggro -= aggroDecayRate * Time.deltaTime;
		}


	}
}
