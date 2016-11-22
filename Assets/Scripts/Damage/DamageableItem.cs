using UnityEngine;
using System.Collections;

public class DamageableItem : MonoBehaviour {
	[SerializeField]
	private float _maxHealth = 100;
	[SerializeField]
	private float _health = 100;
    [SerializeField]
    private float _armour = 0;
    [SerializeField]
    private float _maxArmour = 100;

	private bool dead = false;

	public float health
	{
		get {
			return _health;
		}

		set {
			_health = Mathf.Clamp (value, 0, _maxHealth);
			if (_health == 0) {
				if(!dead)
					die ();
			}
		}
	}

	public float maxHealth {
		get {
			return _maxHealth;
		}

		set {
			_maxHealth = value;

		}
	}

    public float armour
    {
        get
        {
            return _armour;
        }

        set
        {
            _armour = Mathf.Clamp(value, 0, _maxArmour);
        }
    }

    public float maxArmour
    {
        get
        {
            return _maxArmour;
        }
    }


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	}

	public void changeHealth(float value)
	{
		if(armour == 0 || value > 0)
            health += value;
	}

    public void changeArmour(float value)
    {
        armour += value;
    }

	void die()
	{
		dead = true;
		gameObject.SendMessage ("Die", SendMessageOptions.DontRequireReceiver);
	}

}
