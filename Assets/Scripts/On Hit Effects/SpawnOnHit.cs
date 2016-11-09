using UnityEngine;
using System.Collections;

public class SpawnOnHit : MonoBehaviour {

	public GameObject objectToSpawn;
	public Vector3 spawnOffset;
	
	void EndEffect(EndEffectData ed)
	{
		GameObject o = Instantiate<GameObject> (objectToSpawn);
		o.transform.position = transform.TransformPoint (spawnOffset);
	}
}
