using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PrimaryWeaponSystem : MonoBehaviour
{
	public List<Modifier> weaponMods;
	public GunStats baseStats;
	public PrimaryProjectile projectilePrefab;
	public Vector3 barrelPos, barrelDir;
	public bool isContinuous;
	private bool _isFiring = false;
	private float shotTime = 0.0f;

	public GunStats Stats
	{
		get {
			GunStats r = new GunStats();
			if (weaponMods.Count > 0) {
				foreach (var item in weaponMods) {
					r += item.statModifier;
				}
				return baseStats * r;
			} else {
				return baseStats;
			}
		}
	}

	public bool isFiring
	{
		get {
			return _isFiring;
		}

		set {
			if (isContinuous && value && shotTime <= 0 ) {
				shotTime += 1 / Stats.speed;
				for (int i = 0; i < Stats.numProjectiles; i++) {
					SpawnBullet ();
				}
			}
			_isFiring = value;
		}
	}


	void Update()
	{
		if (isFiring && !isContinuous) {
			if (shotTime <= 0) {
				shotTime += 1 / (Stats.speed);
				for (int i = 0; i < Stats.numProjectiles; i++) {
					SpawnBullet (i);
				}
			}
			shotTime -= Time.deltaTime;
		}
		if ((!isFiring || isContinuous) && shotTime > 0) {
			shotTime -= Time.deltaTime;
			if (shotTime <= 0)
				shotTime = 0;
		}

	}

	void SpawnBullet(int bulletNumber = 0)
	{
		PrimaryProjectile g = Instantiate<PrimaryProjectile> (projectilePrefab);
		g.transform.position = transform.TransformPoint (barrelPos);
		Quaternion relativeSpread;
		if (Stats.numProjectiles == 1) {
			relativeSpread = Quaternion.Euler (new Vector3 (0, (Random.value - 0.5f) * 360 * Stats.spread, 0));

		} else {
			relativeSpread = Quaternion.Euler (new Vector3 (0, (360 *(-Stats.spread/2)) + (360 * 2 * Stats.spread * (((float)bulletNumber)/Stats.numProjectiles)), 0));
		}
		g.transform.rotation = Quaternion.FromToRotation (new Vector3 (0, 0, 1), transform.TransformDirection (barrelDir)) * relativeSpread;
		g.pws = this;
		g.relativeSpread = relativeSpread;

		if (gameObject.layer == LayerMask.NameToLayer ("Player"))
			g.gameObject.layer = LayerMask.NameToLayer ("PlayerBullet");
		else if (gameObject.layer == LayerMask.NameToLayer ("Enemy"))
			g.gameObject.layer = LayerMask.NameToLayer ("EnemyBullet");
		else
			g.gameObject.layer = LayerMask.NameToLayer ("NeutralBullet");

	}

	void OnDrawGizmos ()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawCube (transform.TransformPoint (barrelPos), new Vector3 (0.1f, 0.1f, 0.1f));
		Gizmos.DrawRay (transform.TransformPoint (barrelPos), transform.TransformDirection(barrelDir) * 0.5f);
	}
}

