using UnityEngine;
using System.Collections;

[RequireComponent(typeof(DamageableItem))]
public class RenderableHealthBar : OverlayWidget {
	
	public float offset = 0.1f;
	[System.NonSerialized]
	public DamageableItem dmg; //The DamageableItem component of the gameobject

	private HealthBar hb;

	// Use this for initialization
	protected override void Start () {
		base.Start ();
		dmg = GetComponent<DamageableItem> ();
		if ((hb = widgetInstance.GetComponent<HealthBar> ()) == null) {
			print ("No health bar, prep for errors");
		}
	}

	protected override void Update() {
		base.Update ();
		hb.percentFilled = dmg.health / dmg.maxHealth;
	}
}
