using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent(typeof(PrimaryWeaponSystem))]
public class GunCardOverlayWidget :	OverlayWidget {
	PrimaryWeaponSystem pws;
	WeaponStatCard sc;

	protected override void Start () {
		base.Start ();
		pws = GetComponent<DamageableItem> ();
		if ((sc = widgetInstance.GetComponent<WeaponStatCard> ()) == null) {
			sc.Name = pws.Stats.name;
			sc.Damage = "Damage: " & pws.Stats.damage.ToString ();
			sc.Spread = "Spread: " & pws.Stats.spread.ToString ();
			sc.NumberProjectiles = "Number of Projectiles: " & pws.Stats.numProjectiles.ToString ();
			sc.FireRate = "Fire Rate: " & pws.Stats.speed.ToString ();
			sc.Description = pws.Stats.description;
		}
		sc.gameObject.SetActive (false);
	}

	void OnTriggerEnter(Collider other)
	{
		sc.gameObject.SetActive (true);
	}


	void OnTriggerExit(Collider other)
	{
		sc.gameObject.SetActive (false);
	}

}
