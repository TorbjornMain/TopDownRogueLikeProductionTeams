using System.Collections;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class PrimaryBeamProjectile: PrimaryProjectile
{
	public float fadeInTime = 0.1f, fadeOutTime = 0.1f, lifeLoopFrequency = 0.3f;
	public AnimationCurve fadeInWidthCurve, lifeLoopWidthCurve, fadeOutWidthCurve;
	public float beamRange = 10.0f, beamWidth = 0.1f;
	private LineRenderer lr;
	protected RaycastHit rc = new RaycastHit();

	protected override void Start ()
	{
		lr = GetComponent<LineRenderer>();
		lifeLoopWidthCurve.preWrapMode = WrapMode.Loop;
		lifeLoopWidthCurve.postWrapMode = WrapMode.Loop;
		base.Start ();
	}

	protected override void Update ()
	{
		transform.position = (pws.transform.TransformPoint (pws.barrelPos));
		transform.rotation = (Quaternion.FromToRotation (new Vector3 (0, 0, 1), pws.transform.TransformDirection (pws.barrelDir)) * relativeSpread);

		bool raycastHit;
		if (isPiercing) {
			raycastHit = Physics.Raycast (pws.barrelPos, pws.barrelDir, out rc, beamRange, LayerMask.GetMask ("Terrain"));
		} else {
			raycastHit = Physics.Raycast (pws.barrelPos, pws.barrelDir, out rc, beamRange, 1 << pws.gameObject.layer);
		}
		if (raycastHit) {
			lr.SetPositions (new Vector3[]{transform.position, transform.position + transform.forward * rc.distance});
		} else {
			lr.SetPositions (new Vector3[]{transform.position, transform.position + transform.forward * beamRange});
		}

		float lrWidth;
		if (timeAlive < fadeInTime) {
			lrWidth = fadeInWidthCurve.Evaluate (timeAlive / fadeInTime) * beamWidth;
		} else if (timeAlive < lifetime + fadeInTime || (pws.isContinuous && !hasStoppedFiring)) {
			lrWidth = lifeLoopWidthCurve.Evaluate ((timeAlive - fadeInTime) / lifeLoopFrequency) * beamWidth;
		} else {
			lrWidth = fadeOutWidthCurve.Evaluate ((timeAlive - (lifetime + fadeInTime)) / fadeOutTime) * beamWidth;
		}
		lr.SetWidth (lrWidth, lrWidth);


		timeAlive += Time.deltaTime;

		if (!pws.isFiring) {
			hasStoppedFiring = true;
		}
		if (pws.isContinuous) {
			//Do Boxcast and apply damage to all enemies hit (damage * speed * Time.deltaTime)
			if (!hasStoppedFiring)
				lifetime = timeAlive - fadeInTime;
		}

		if(timeAlive >= fadeInTime + lifetime + fadeOutTime)
		{
			endEffect();
		}

		base.Update ();

	}

	protected override void endEffect ()
	{
		base.endEffect ();
	}
}

