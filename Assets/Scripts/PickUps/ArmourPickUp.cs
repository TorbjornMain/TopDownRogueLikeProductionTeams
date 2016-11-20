using UnityEngine;
using System.Collections;

public class ArmourPickUp : MonoBehaviour {
    HealthBar playersArmour;
    GameObject player;
    DamageableItem playersDamageComponent;
    public float armourAmount;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }
        if (player != null && playersDamageComponent == null)
        {
            playersDamageComponent = player.GetComponent<DamageableItem>();
        }
    }

    void OnTriggerEnter(Collider player)
    {
        if (player.tag == "Player" && playersDamageComponent.armour < playersDamageComponent.maxArmour)
        {
            if ((playersDamageComponent.armour + armourAmount) > playersDamageComponent.maxArmour)
            {
                playersDamageComponent.armour = playersDamageComponent.maxArmour;
                DestroyObject(gameObject);
            }
            else
            {
                playersDamageComponent.armour = playersDamageComponent.armour + armourAmount;
                DestroyObject(gameObject);
            }
        }
    }
}
