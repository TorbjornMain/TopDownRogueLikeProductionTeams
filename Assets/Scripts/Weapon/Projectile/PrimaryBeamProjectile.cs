﻿using System.Collections;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class PrimaryBeamProjectile: PrimaryProjectile
{
	public float fadeInTime = 0.1f, fadeOutTime = 0.1f, lifeLoopFrequency = 0.3f;
	public AnimationCurve fadeInWidthCurve, lifeLoopWidthCurve, fadeOutWidthCurve;
	public float beamRange = 10.0f, beamWidth = 0.1f;
    public int beamRes = 10;
	private LineRenderer lr;
	protected RaycastHit rc = new RaycastHit();
	private LayerMask hitLayer;
    protected Vector3[] beamPositions;

    protected override void Start ()
	{
        beamPositions = new Vector3[beamRes];
        hitLayer = ~((1 << pws.gameObject.layer) | LayerMask.GetMask ("PlayerBullet", "EnemyBullet", "NeutralBullet", "Item"));
		lr = GetComponent<LineRenderer>();
        lr.positionCount = beamRes;
		lifeLoopWidthCurve.preWrapMode = WrapMode.Loop;
		lifeLoopWidthCurve.postWrapMode = WrapMode.Loop;
		base.Start ();
	}

	protected override void Update ()
	{
		if (pws == null) {
			Destroy (gameObject);
			return;
		}
		if (pws.gameObject.layer == LayerMask.NameToLayer("Item")) {
			Destroy (gameObject);
			return;
		}

		transform.position = (pws.transform.TransformPoint (pws.barrelPos));
		transform.rotation = (Quaternion.FromToRotation (new Vector3 (0, 0, 1), pws.transform.TransformDirection (pws.barrelDir)) * relativeSpread);
		float range;
		bool raycastHit;
		if (isPiercing) {
			raycastHit = Physics.Raycast (transform.position, transform.forward, out rc, beamRange, LayerMask.GetMask ("Terrain"));
		} else {
			raycastHit = Physics.Raycast (transform.position, transform.forward, out rc, beamRange, hitLayer);
		}
		if (raycastHit) {
			range = rc.distance;
            
            for(int i = 0; i < beamRes; i++)
            {
                beamPositions[i] = Vector3.Lerp(transform.position, transform.position + transform.forward * rc.distance, (float)i / (float)beamRes);
            }

			lr.SetPositions (beamPositions);
		} else {
			range = beamRange;
            for (int i = 0; i < beamRes; i++)
            {
                beamPositions[i] = Vector3.Lerp(transform.position, transform.position + transform.forward * beamRange, (float)i / (float)beamRes);
            }
            lr.SetPositions (beamPositions);
		}

		float lrWidth;
		if (timeAlive < fadeInTime) {
			lrWidth = fadeInWidthCurve.Evaluate (timeAlive / fadeInTime) * beamWidth;
		} else if (timeAlive < lifetime + fadeInTime || (pws.isContinuous && !hasStoppedFiring)) {
			lrWidth = lifeLoopWidthCurve.Evaluate ((timeAlive - fadeInTime) / lifeLoopFrequency) * beamWidth;

			RaycastHit[] rci = Physics.BoxCastAll (transform.position, new Vector3 (lrWidth / 2, 0.5f, 1.0f), transform.forward, transform.rotation, range, hitLayer);
			EndEffectData ed;
			foreach (var item in rci) {
				ed = new EndEffectData ();
				ed.targ = item.collider.gameObject;
				ed.pos = item.point;
				ed.rot = transform.rotation;
				if (pws.isContinuous) {
					ed.magnitude = pws.Stats.damage * Time.deltaTime;
				} else {
					ed.magnitude = pws.Stats.damage * (Time.deltaTime / lifetime);
				}
				SendMessage ("EndEffect", ed);
			}

			ed = new EndEffectData ();
			ed.targ = null;
			ed.pos = transform.position + transform.forward * range;
			ed.rot = transform.rotation;
			ed.magnitude = pws.Stats.damage;
			SendMessage ("EndEffect", ed);

		} else {
			lrWidth = fadeOutWidthCurve.Evaluate ((timeAlive - (lifetime + fadeInTime)) / fadeOutTime) * beamWidth;
		}
        lr.widthMultiplier = lrWidth;




		timeAlive += Time.deltaTime;

		if (!pws.isFiring) {
			hasStoppedFiring = true;
		}
		if (pws.isContinuous) {
			if (!hasStoppedFiring)
				lifetime = timeAlive - fadeInTime;

		}




		if(timeAlive >= fadeInTime + lifetime + fadeOutTime)
		{
			Destroy (gameObject);
		}

		base.Update ();

	}


}

