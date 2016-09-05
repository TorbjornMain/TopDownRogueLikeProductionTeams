using UnityEngine;
using System.Collections;


[RequireComponent(typeof(Transport))]
[DisallowMultipleComponent()]
public class TransportNode : MonoBehaviour {

	[System.NonSerialized]
	public Transport nodeObject;
	[System.NonSerialized]
	public bool isAttached;
	public Vector3 offset;
	// Use this for initialization
	void Awake () {
		nodeObject = GetComponent<Transport>();
	}
	
	void OnDrawGizmos()
	{
		Gizmos.color = Color.green;
		Gizmos.DrawSphere(transform.TransformPoint(offset), 0.2f);
	}
}
