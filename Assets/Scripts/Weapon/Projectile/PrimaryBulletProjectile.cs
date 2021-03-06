﻿using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PrimaryBulletProjectile : PrimaryProjectile
{
	public AnimationCurve projectileSpeedCurve;
	public float baseProjectileSpeed = 1.0f;
	private Rigidbody rb;
	public bool useGravity = false;

	protected override void Start()
	{
		rb = GetComponent<Rigidbody> ();
		base.Start ();
	}

	protected override void Update ()
	{
		rb.velocity = transform.forward * projectileSpeedCurve.Evaluate (timeAlive / lifetime) * baseProjectileSpeed + Physics.gravity;
		if (!pws.isFiring) {
			hasStoppedFiring = true;
		}
		if ((pws.isContinuous && hasStoppedFiring) || !pws.isContinuous) {
			timeAlive += Time.deltaTime;
			if (timeAlive > lifetime) {
				Destroy (gameObject);
			}
		}
		base.Update ();
	}

	protected void OnTriggerEnter(Collider other)
	{
		endEffect (other.gameObject);
		if (!isPiercing || (other.gameObject.layer == LayerMask.NameToLayer("Terrain")))
			Destroy (gameObject);
	}

	protected void OnCollisionEnter(Collision other)
	{
		bounceEffect (other.gameObject);
	}

	private void endEffect(GameObject targ)
	{
		EndEffectData ed = new EndEffectData();
		ed.targ = targ;
		ed.pos = transform.position;
		ed.rot = transform.rotation;
		ed.magnitude = pws.Stats.damage;
		gameObject.SendMessage ("EndEffect", ed); 
	}

	virtual protected void bounceEffect(GameObject targ)
	{
		EndEffectData ed = new EndEffectData();
		ed.pos = transform.position;
		ed.rot = transform.rotation;
		ed.magnitude = pws.Stats.damage;
		gameObject.SendMessage ("BounceEffect", ed); 
	}
}


