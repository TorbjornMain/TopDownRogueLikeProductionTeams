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
	public string name;
	public TransportType type;
	public float speed;
	public float turnRate;
	public string description;
}

[DisallowMultipleComponent()]
public class Transport : EquippableItem {

	private Vector3 footRotVec;

	[System.NonSerialized]
	public CharacterController mainBody;

	public TransportStats stats;

	[System.NonSerialized]
	public float transportSpeed;


	private Vector3 previousPos;


	void Start()
	{
		previousPos = transform.position;
	}

	void OnTriggerStay(Collider other)
	{
		if (gameObject.layer == LayerMask.NameToLayer ("Item")) {
			if (Input.GetButtonDown ("EquipLeft") || Input.GetButtonDown("EquipRight")) {
				transform.localRotation = Quaternion.Euler (0, 0, 0);
				BodySockets oBS = other.GetComponent<BodySockets> ();
				oBS.DetachTransport (false);
				oBS.AttachTransport (GetComponent<TransportNode>());
			}
		}
	}

	public void Drive(Vector3 direction)
	{
		transportSpeed = (transform.position - previousPos).magnitude / Time.deltaTime;
		if (direction.magnitude > 0) {
			footRotVec = (direction.normalized + footRotVec).normalized;
			transform.rotation = Quaternion.Euler (0, Quaternion.FromToRotation (new Vector3 (0, 0, 1), Vector3.Slerp (transform.forward, footRotVec, stats.turnRate)).eulerAngles.y, 0);
			mainBody.Move ((transform.forward * stats.speed * Time.deltaTime));
			mainBody.Move (-new Vector3 (0, mainBody.transform.position.y, 0));
		} else if (mainBody.velocity.magnitude > 0) {
			Quaternion.Euler (0, Quaternion.FromToRotation (new Vector3 (0, 0, 1), Vector3.Slerp (transform.forward, mainBody.velocity, stats.turnRate * 2)).eulerAngles.y, 0);
		}
		previousPos = transform.position;
	}
}
