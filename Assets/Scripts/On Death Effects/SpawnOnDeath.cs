using System.Collections;
using UnityEngine;

[RequireComponent(typeof(DamageableItem))]
public class SpawnOnDeath : MonoBehaviour {
	public GameObject objectToSpawn;
	public Vector3 spawnOffset;

	void Die()
	{
		GameObject o = Instantiate<GameObject> (objectToSpawn);
		o.transform.position = transform.TransformPoint (spawnOffset);
	}
}

