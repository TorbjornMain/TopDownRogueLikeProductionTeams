using UnityEngine;
using System.Collections;

public class DestroyOnHit : MonoBehaviour {
	void EndEffect(EndEffectData ed)
	{
		Destroy (gameObject);
	}
}
