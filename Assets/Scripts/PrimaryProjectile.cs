using System.Collections;
using UnityEngine;



public class PrimaryProjectile : MonoBehaviour
{
	[System.NonSerialized]
	public PrimaryWeaponSystem pws;
	[System.NonSerialized]
	public Quaternion relativeSpread;
	protected float timeAlive = 0.0f;
	public float lifetime = 1.0f;
	public bool isPiercing;
	protected bool hasStoppedFiring = false;


	protected virtual void Start()
	{
	}


	protected virtual void Update()
	{


	}
}

