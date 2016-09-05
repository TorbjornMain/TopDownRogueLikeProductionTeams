using UnityEngine;
using System.Collections;

[DisallowMultipleComponent()]
[RequireComponent(typeof(PrimaryWeaponSystem))]
public class PrimaryWeaponNode : MonoBehaviour {
	public Vector3 offset;
	[System.NonSerialized]
	public PrimaryWeaponSystem nodeObject;
	[System.NonSerialized]
	public bool isAttached;


	void Awake()
	{
		nodeObject = GetComponent<PrimaryWeaponSystem> ();
	}

	void OnDrawGizmos()
	{
		Gizmos.color = Color.magenta;
		Gizmos.DrawSphere(transform.TransformPoint(offset), 0.2f);
	}
}
