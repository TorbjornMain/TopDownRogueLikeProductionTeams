using UnityEngine;
using System.Collections;

public class DamageableItem : MonoBehaviour {
	[SerializeField]
	private float _maxHealth = 100;
	[SerializeField]
	private float _health = 100;

	public float health
	{
		get {
			return _health;
		}

		set {
			_health = Mathf.Clamp (value, 0, _maxHealth);
			if (_health == 0) {
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


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	}

	public void changeHealth(float value)
	{
		health += value;
	}

	void die()
	{
		gameObject.SendMessage ("Die", SendMessageOptions.DontRequireReceiver);
	}

}
