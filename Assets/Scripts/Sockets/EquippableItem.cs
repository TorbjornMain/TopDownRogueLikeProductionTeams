using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Collider))]
public class EquippableItem : MonoBehaviour {
	public float itemFadeTime = 10.0f;
	private Collider col;

	// Use this for initialization
	void Start () {
		col = GetComponent<Collider> ();
	}
	
	// Update is called once per frame
	protected virtual void Update()
	{
		if (gameObject.layer == LayerMask.NameToLayer ("Item")) {
			transform.rotation = Quaternion.Euler (0, transform.rotation.eulerAngles.y + 180 * Time.deltaTime, 0);
			col.enabled = true;
		} else {
			col.enabled = false;
		}

	}

	protected virtual void OnAttach()
	{
	}

	protected virtual void OnDetach()
	{
	}

	IEnumerator Despawn()
	{
		yield return new WaitForSeconds (itemFadeTime);
		if (gameObject.layer == LayerMask.NameToLayer ("Item")) {
			Destroy (this.gameObject);
		}

	}
}
