using UnityEngine;
using System.Collections;


[RequireComponent(typeof(DamageDealer))]
[RequireComponent(typeof(SphereCollider))]
public class Explosion : MonoBehaviour {

	public float lifetime;
	public float baseDamage;

	private DamageDealer dd;
	private SphereCollider col;

	void Start()
	{
		dd = GetComponent<DamageDealer> ();
		col = GetComponent<SphereCollider> ();
		StartCoroutine (Despawn ());
	}

	IEnumerator Despawn()
	{
		yield return new WaitForSeconds (lifetime);
		Destroy (gameObject);
	}


	void OnTriggerEnter(Collider other)
	{
		DamageableItem dmgTarg;
		if ((dmgTarg = other.GetComponent<DamageableItem> ()) != null) {
			DamageInfo dmg = new DamageInfo ();
			dmg.baseDamage = baseDamage * (Mathf.Clamp01(col.radius - (other.transform.position - transform.position).magnitude) / col.radius);
			dmg.target = dmgTarg;
			dd.DealDamage (dmg);
		}
	}
}
