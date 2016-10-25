using UnityEngine;
using System.Collections;

public enum TransportType
{
	Tank,
	Legs,
	Hover
}

[System.Serializable]
public class TransportStats
{
	public TransportType type;
	public float speed;
	public float turnRate;
}

[DisallowMultipleComponent()]
[RequireComponent(typeof(MeshRenderer))]
public class Transport : MonoBehaviour {

	private Vector3 footRotVec;

	[System.NonSerialized]
	public CharacterController mainBody;

	public TransportStats stats;

	public float itemFadeTime = 10.0f;


	private MeshRenderer mr;

	void Start()
	{
		mr = new MeshRenderer ();
	}

	void Update()
	{
		if (gameObject.layer == LayerMask.NameToLayer ("Item")) {
			transform.rotation = Quaternion.Euler (0, transform.rotation.eulerAngles.y + 180 * Time.deltaTime, 0);
		}

	}

	IEnumerator Despawn()
	{
		yield return new WaitForSeconds (itemFadeTime);
		Destroy (this.gameObject);

	}

	public void Drive(Vector3 direction)
	{
		if (direction.magnitude > 0) {
			footRotVec = (direction.normalized + footRotVec).normalized;
			transform.rotation = Quaternion.Euler (0, Quaternion.FromToRotation (new Vector3 (0, 0, 1), Vector3.Slerp (transform.forward, footRotVec, stats.turnRate)).eulerAngles.y, 0);
			mainBody.Move ((transform.forward * stats.speed * Time.deltaTime));
			mainBody.Move (-new Vector3 (0, mainBody.transform.position.y, 0));
		} else if (mainBody.velocity.magnitude > 0) {
			Quaternion.Euler (0, Quaternion.FromToRotation (new Vector3 (0, 0, 1), Vector3.Slerp (transform.forward, mainBody.velocity, stats.turnRate * 2)).eulerAngles.y, 0);
		}
	}
}
