using UnityEngine;
using System.Collections;

[RequireComponent(typeof(DamageableItem))]
public class DestroyOnDeath : MonoBehaviour {
	void Die()
	{
		Destroy (this);
	}
}
