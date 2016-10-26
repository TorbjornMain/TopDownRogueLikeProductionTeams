using UnityEngine;
using System.Collections;

[DisallowMultipleComponent()]
[RequireComponent(typeof(BodySockets))]
public class DropItemsOnDeath : MonoBehaviour {

	public float dropRate = 0.1f;

	private BodySockets bs;

	void Start()
	{
		bs = GetComponent<BodySockets> ();
	}

	void Die ()
	{
		if (Random.value <= dropRate) {
			int rand = Random.Range (0, bs.primaryWeaponSockets.Length + 1);
			if (rand == bs.primaryWeaponSockets.Length) {
				bs.DetachTransport (false);
			} else {
				bs.DetachPrimaryWeapon (rand, false);
			}
		}
	}
}
