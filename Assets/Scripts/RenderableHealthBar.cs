﻿using UnityEngine;
using System.Collections;

[RequireComponent(typeof(DamageableItem))]
public class RenderableHealthBar : MonoBehaviour {

	public float offset = 0.1f;
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

	void OnDestroy () {
		EnemyManager.healthBars.Remove(this);
	}
}
