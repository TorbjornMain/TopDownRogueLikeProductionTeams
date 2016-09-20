using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PrimaryBulletProjectile : PrimaryProjectile
{
	public AnimationCurve projectileSpeedCurve;
	public float baseProjectileSpeed = 1.0f;
	private Rigidbody rb;

	protected override void Start()
	{
		rb = GetComponent<Rigidbody> ();
		base.Start ();
	}

	protected override void Update ()
	{
		rb.velocity = transform.forward * projectileSpeedCurve.Evaluate (timeAlive / lifetime) * baseProjectileSpeed;
		if (!pws.isFiring) {
			hasStoppedFiring = true;
		}
		if ((pws.isContinuous && hasStoppedFiring) || !pws.isContinuous) {
			timeAlive += Time.deltaTime;
			if(timeAlive > lifetime)
				endEffect (null);
		}
		base.Update ();
	}

	protected void OnTriggerEnter(Collider other)
	{
		endEffect (other.gameObject);
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
}


