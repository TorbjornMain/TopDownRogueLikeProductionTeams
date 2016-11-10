using UnityEngine;
using System.Collections;

[RequireComponent(typeof(DamageableItem))]
public class DissapearOnDeath : MonoBehaviour {

	void Die()
	{
		MeshRenderer[] mr = GetComponentsInChildren<MeshRenderer> ();
		if (mr.Length > 0) {
			foreach (var item in mr) {
				item.enabled = false;
			}
		}
		TopDownController tc = GetComponent<TopDownController> ();
		if (tc) {
			tc.enabled = false;
		}
		BodySockets bs = GetComponent<BodySockets> ();
		if (bs) {
			bs.transportSocket.transport.gameObject.SetActive (false);
			foreach (var item in bs.primaryWeaponSockets) {
				item.primaryWeapon.gameObject.SetActive (false);
			}
		}
		Collider col = GetComponent<Collider> ();
		if (col) {
			col.enabled = false;
		}
	}
}
