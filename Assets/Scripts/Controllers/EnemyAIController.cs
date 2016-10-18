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
	private DamageableItem dmg;




	// Use this for initialization
	void Start () {
		bs = GetComponent<BodySockets> ();
		sp = GetComponent<SpawnableObject> ();
		dmg = GetComponent<DamageableItem> ();
	}
	
	// Update is called once per frame
	void Update () {
		float playerDistance = (sp.lm.playerInstance.transform.position - transform.position).magnitude;
		if (playerDistance < aggroRadius) {
			aggro += aggroGainRate * Time.deltaTime;
		} else {
			aggro -= aggroDecayRate * Time.deltaTime;
		}

		if (aggro > aggroThreshold) {
			if (dmg.health / dmg.maxHealth < fleeThreshold) {
				state = EnemyAIState.Flee;
			} else {
				state = EnemyAIState.Pursue;
			}
		} else {
			state = EnemyAIState.Wander;
		}

		switch (state) {
		case EnemyAIState.Wander:
			
			break;
		case EnemyAIState.Pursue:
			bs.transportSocket.transport.nodeObject.Drive((sp.lm.playerInstance.transform.position - this.transform.position).normalized);
			break;
		case EnemyAIState.Flee:
			bs.transportSocket.transport.nodeObject.Drive((this.transform.position - sp.lm.playerInstance.transform.position).normalized);
			break;
		default:
			break;
		}


	}
}
