using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent(typeof(PrimaryWeaponSystem))]
public class GunCardOverlayWidget :	OverlayWidget {
	PrimaryWeaponSystem pws;
	WeaponStatCard sc;

	protected override void Start () {
		base.Start ();
		pws = GetComponent<PrimaryWeaponSystem> ();
		if ((sc = widgetInstance.GetComponent<WeaponStatCard> ()) == null) {
			sc.Name.text = pws.Stats.name;
			sc.Damage.text = "Damage: " + pws.Stats.damage.ToString ();
			sc.Spread.text = "Spread: " + pws.Stats.spread.ToString ();
			sc.NumberProjectiles.text = "Number of Projectiles: " + pws.Stats.numProjectiles.ToString ();
			sc.FireRate.text = "Fire Rate: " + pws.Stats.speed.ToString ();
			sc.Description.text = pws.Stats.description;
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
