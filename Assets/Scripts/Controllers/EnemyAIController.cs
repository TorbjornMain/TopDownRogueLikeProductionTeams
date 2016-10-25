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
	public float triggerHappiness = 1.0f;
	public float triggerInertia = 1.0f;
	public float aggroGainRate = 0.1f;
	public float aggroDecayRate = 0.1f;
	public float wanderDistance = 4;
	public float wanderStagnation = 6;


	private float timeInState = 0.0f;
	private float aggro = 0.0f;
	private EnemyAIState state = EnemyAIState.Wander;
	private BodySockets bs;
	private SpawnableObject sp;
	private DamageableItem dmg;

	private Vector3 wanderDest = new Vector3 ();
	private float wanderTime = 0.0f;


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
			aggro = Mathf.Max (aggro, 0);
		}

		if (aggro > aggroThreshold) {
			if (dmg.health / dmg.maxHealth < fleeThreshold) {
				state = EnemyAIState.Flee;
			} else {
				state = EnemyAIState.Pursue;
			}
			this.transform.rotation = Quaternion.LookRotation ((sp.lm.playerInstance.transform.position - transform.position).normalized);
			if (bs.isFiring && Random.value < (timeInState/triggerInertia) / (1 + (triggerHappiness * aggro))) {
				timeInState = 0.0f;
				bs.CeaseFirePrimary ();
			}
			if (!bs.isFiring && Random.value < (triggerHappiness * aggro * timeInState) / triggerInertia) {
				timeInState = 0.0f;
				bs.StartFirePrimary ();
			}
			timeInState += Time.deltaTime;
		} else {
			bs.CeaseFirePrimary ();
			state = EnemyAIState.Wander;
			timeInState = 0.0f;
		}

		switch (state) {
		case EnemyAIState.Wander:
			if (wanderTime <= 0) {
				Vector3 r = Random.onUnitSphere;
				r.y = 0;
				wanderDest = transform.position + r * wanderDistance;
				wanderTime = Random.value * wanderStagnation;
			}

			break;
		case EnemyAIState.Pursue:
			if (wanderTime <= 0) {
				Vector3 r = Random.onUnitSphere;
				r.y = 0;
				wanderDest = transform.position + (r + ((sp.lm.playerInstance.transform.position - this.transform.position).normalized * aggro)).normalized * wanderDistance;
				wanderTime = Random.value * wanderStagnation/ (aggro + 0.1f);
			}

			break;
		case EnemyAIState.Flee:
			if (wanderTime <= 0) {
				Vector3 r = Random.onUnitSphere;
				r.y = 0;
				wanderDest = transform.position + (r + (( this.transform.position - sp.lm.playerInstance.transform.position).normalized * aggro)).normalized * wanderDistance;
				wanderTime = Random.value * wanderStagnation / (aggro + 0.1f);
			}
			break;
		default:
			break;
		}
		if ((wanderDest - transform.position).magnitude > 0.3) {
			bs.transportSocket.transport.nodeObject.Drive ((wanderDest - transform.position).normalized);
		}
		wanderTime -= Time.deltaTime;
	}
}
