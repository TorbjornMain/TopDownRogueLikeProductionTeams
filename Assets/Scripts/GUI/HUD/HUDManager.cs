using UnityEngine;
using System.Collections;

public class HUDManager : MonoBehaviour {
    public GameObject player;
    public GameObject healthBar;
    HealthBar health;
    float playerHealth = -1;
    float playerMaxHealth;
    DamageableItem playersDamageComponent;

    public GameObject armourBar;
    float playerArmor;
    HealthBar armour;

    // Use this for initialization
    void Start () {
        health = healthBar.GetComponent<HealthBar>();
        armour = armourBar.GetComponent<HealthBar>();
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
            playerMaxHealth = playersDamageComponent.maxHealth;
        }
        if(playerHealth >= 0)
        {
            health.percentFilled = playersDamageComponent.health / playerMaxHealth;
            //health.percentFilled = playersDamageComponent.health;
        }
        
    }

    float getArmor()
    {
        return playerArmor;
    }
}
