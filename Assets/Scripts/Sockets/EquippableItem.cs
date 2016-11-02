using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Collider))]
public class EquippableItem : MonoBehaviour {
	public float itemFadeTime = 10.0f;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	protected virtual void Update()
	{
		if (gameObject.layer == LayerMask.NameToLayer ("Item")) {
			transform.rotation = Quaternion.Euler (0, transform.rotation.eulerAngles.y + 180 * Time.deltaTime, 0);
		}

	}

	void OnTriggerEnter(Collider other)
	{
		if (gameObject.layer == LayerMask.NameToLayer ("Item")) {

		}
	}


	void OnTriggerEnter(Collider other)
	{
		if (gameObject.layer == LayerMask.NameToLayer ("Item")) {

		}
	}

	IEnumerator Despawn()
	{
		yield return new WaitForSeconds (itemFadeTime);
		Destroy (this.gameObject);

	}
}
