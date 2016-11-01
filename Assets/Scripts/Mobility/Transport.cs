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

	public void Drive(Vector3 direction)
	{
		transportSpeed = (transform.position - previousPos).magnitude;
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
