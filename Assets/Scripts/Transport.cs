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
public class Transport : MonoBehaviour {

	private Vector3 footRotVec;

	[System.NonSerialized]
	public Rigidbody mainBody;

	public TransportStats stats;

	public void Drive(Vector3 direction)
	{
		if (direction.magnitude > 0) {
			footRotVec = (direction.normalized + footRotVec).normalized;

			transform.rotation = Quaternion.Euler (0, Quaternion.FromToRotation (new Vector3 (0, 0, 1), Vector3.Slerp (transform.forward, footRotVec, stats.turnRate)).eulerAngles.y, 0);


			mainBody.AddForce (Time.deltaTime * ((transform.forward * stats.speed) - new Vector3 (mainBody.velocity.x, 0, mainBody.velocity.z)), ForceMode.VelocityChange);
		} else if (mainBody.velocity.magnitude > 0) {
			Quaternion.Euler (0, Quaternion.FromToRotation (new Vector3 (0, 0, 1), Vector3.Slerp (transform.forward, mainBody.velocity, stats.turnRate * 2)).eulerAngles.y, 0);
		}
	}
}
