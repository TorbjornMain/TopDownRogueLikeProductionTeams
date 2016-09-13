using UnityEngine;
using System.Collections;

[RequireComponent(typeof(DamageableItem))]
public class RenderableHealthBar : MonoBehaviour {

	public float offset = 0.1f;
	public DamageableItem dmg;

	// Use this for initialization
	void Start () {
		dmg = GetComponent<DamageableItem> ();
		EnemyManager.healthBars.Add(this);
	}

	void OnDestroy () {
		EnemyManager.healthBars.Remove(this);
	}
}
