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
		playersDamageComponent.changeArmour (armourAmount);
		Destroy (gameObject);
    }
}
