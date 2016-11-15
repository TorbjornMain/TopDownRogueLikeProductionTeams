﻿using UnityEngine;
using System.Collections;

public class HUDManager : MonoBehaviour {
    public GameObject player;
    public GameObject healthBar;
    HealthBar health;
    float playerHealth = -1;
    DamageableItem playersDamageComponent;

    // Use this for initialization
    void Start () {
        health = healthBar.GetComponent<HealthBar>();
	}
	
	// Update is called once per frame
	void Update () {
	    if(player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }
        if(player != null && playerHealth == -1)
        {
            playersDamageComponent = player.GetComponent<DamageableItem>();
            playerHealth = playersDamageComponent.health;
        }
        if(playerHealth >= 0)
        {
            health.percentFilled = playersDamageComponent.health;
        }
        
    }
}