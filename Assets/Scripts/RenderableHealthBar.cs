using UnityEngine;
using System.Collections;

[RequireComponent(typeof(DamageableItem))]
public class RenderableHealthBar : MonoBehaviour {

	public float offset = 0.1f;
	[System.NonSerialized]
	public DamageableItem dmg;
	public HealthBar healthBarPrefab;
	[System.NonSerialized]
	public HealthBar healthBarInstance;


	// Use this for initialization
	void Start () {
		healthBarInstance = Instantiate<HealthBar> (healthBarPrefab);
		dmg = GetComponent<DamageableItem> ();
		EnemyManager.healthBars.Add(this);
	}

	void Update(){
		healthBarInstance.percentFilled = dmg.health / dmg.maxHealth;
	}

	void OnDestroy () {
		EnemyManager.healthBars.Remove(this);
		if(healthBarInstance != null)
			Destroy (healthBarInstance.gameObject);
	}
}
