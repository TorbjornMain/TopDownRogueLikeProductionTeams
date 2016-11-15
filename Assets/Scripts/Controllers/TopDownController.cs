using UnityEngine;
using System.Collections;

[RequireComponent(typeof(BodySockets))]
[RequireComponent(typeof(Animator))]
[DisallowMultipleComponent()]
public class TopDownController : MonoBehaviour {
	public Camera playerCamera;
	public Vector3 cameraOffset, cameraAngle;
	[System.NonSerialized]
	public BodySockets mainBody;
	private const float speed = 15;
	private Vector3 gunAimVec = new Vector3();
	private bool isFiringPrimaries;
	private Animator a;

	// Use this for initialization
	void Start () {
		mainBody = GetComponent<BodySockets> ();
		a = GetComponent<Animator> ();
	}

	void Update () {
		Vector3 moveVec = new Vector3 (Input.GetAxis ("Horizontal"), 0, Input.GetAxis ("Vertical"));
		if(mainBody.transportSocket.transport != null)
			mainBody.transportSocket.transport.nodeObject.Drive(moveVec);

		playerCamera.transform.position = mainBody.transform.position + cameraOffset;
		playerCamera.transform.rotation = Quaternion.Euler (cameraAngle);

		if ((Input.GetJoystickNames ().Length == 0) || !Settings.bUsingController) {
			gunAimVec = Input.mousePosition - playerCamera.WorldToScreenPoint (mainBody.transform.position);
		} else {
			gunAimVec = new Vector3 (Input.GetAxis("RightStick-X"), 0, Input.GetAxis("RightStick-Y"));
		}
		gunAimVec.Set (gunAimVec.x, 0, gunAimVec.y);
		mainBody.transform.rotation = Quaternion.Euler (0, Quaternion.FromToRotation (new Vector3 (0, 0, 1), gunAimVec).eulerAngles.y, 0);
		a.SetFloat ("Speed", moveVec.magnitude * speed);

		if ((Input.GetJoystickNames ().Length == 0) || !Settings.bUsingController) {
			if (Input.GetButtonDown ("Fire1")) {
				mainBody.StartFirePrimary ();
			}

			if (Input.GetButtonUp ("Fire1")) {
				mainBody.CeaseFirePrimary ();
			}
		} else {
			if (gunAimVec.magnitude > 0.2f && !mainBody.isFiring) {
				mainBody.StartFirePrimary ();
			}
			if (gunAimVec.magnitude < 0.2f && mainBody.isFiring) {
				mainBody.CeaseFirePrimary ();
			}
		}
	
	}
}
