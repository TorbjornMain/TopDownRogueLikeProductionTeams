using UnityEngine;
using System.Collections;

public class HealthPickup : MonoBehaviour {
    HealthBar playersHealth;
    GameObject player;
    DamageableItem playersDamageComponent;
    public float healAmount;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }
        if(player != null && playersDamageComponent == null)
        {
            playersDamageComponent = player.GetComponent<DamageableItem>();
        }
    }

    void OnTriggerEnter(Collider player)
    {
        if(player.tag == "Player" && playersDamageComponent.health < playersDamageComponent.maxHealth)
        {
            if((playersDamageComponent.health + healAmount) > playersDamageComponent.maxHealth)
            {
                playersDamageComponent.health = playersDamageComponent.maxHealth;
                DestroyObject(gameObject);
            }
            else {
                playersDamageComponent.health = playersDamageComponent.health + healAmount;
                DestroyObject(gameObject);
            }
        }
    }
}
