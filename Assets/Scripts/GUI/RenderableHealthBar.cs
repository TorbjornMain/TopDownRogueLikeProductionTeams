using UnityEngine;
using System.Collections;

[RequireComponent(typeof(DamageableItem))]
public class RenderableHealthBar : MonoBehaviour {
	
	public float offset = 0.1f;
	[System.NonSerialized]
	public DamageableItem dmg; //The DamageableItem component of the gameobject
	public HealthBar healthBarPrefab; //The health bar prefab
	[System.NonSerialized]
	public HealthBar healthBarInstance; //The instance of the health bar prefab


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
