using UnityEngine;
using System.Collections;

[RequireComponent(typeof(DamageDealer))]
public class DamageOnHit : MonoBehaviour {

	private DamageDealer dmg;


	void Start()
	{
		dmg = GetComponent<DamageDealer> ();
	}

	void EndEffect(EndEffectData ed)
	{
		if (ed.targ == null)
			return;
		DamageInfo d = new DamageInfo();
		d.baseDamage = ed.magnitude;
		DamageableItem di = ed.targ.GetComponent<DamageableItem> ();
		if(di != null)
		{
			d.target = di;
			dmg.DealDamage (d);
		}
	}
}
