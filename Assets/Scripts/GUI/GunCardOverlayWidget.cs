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
		OverlayManager.widgets.Remove(this);
		sc.gameObject.SetActive (false);
	}

	void OnTriggerEnter(Collider other)
	{
		if (gameObject.layer == LayerMask.NameToLayer ("Item")) {
			OverlayManager.widgets.Add (this);
			sc.gameObject.SetActive (true);
			sc.Name.text = pws.Stats.name;
			sc.Damage.text = "Damage: " + pws.Stats.damage.ToString ();
			sc.Spread.text = "Spread: " + pws.Stats.spread.ToString ();
			sc.NumberProjectiles.text = "Number of Projectiles: " + pws.Stats.numProjectiles.ToString ();
			sc.FireRate.text = "Fire Rate: " + pws.Stats.speed.ToString ();
			sc.Description.text = pws.Stats.description;
		}
	}


	void OnTriggerExit(Collider other)
	{
		if (gameObject.layer == LayerMask.NameToLayer ("Item")) {
			OverlayManager.widgets.Remove (this);
			sc.gameObject.SetActive (false);
		}
	}

}
