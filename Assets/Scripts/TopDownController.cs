using UnityEngine;
using System.Collections;

[DisallowMultipleComponent()]
public class TopDownController : MonoBehaviour {
	public Camera playerCamera;
	public Vector3 cameraOffset, cameraAngle;
	public BodySockets mainBody;
	public float speed = 5;
	private Vector3 gunAimVec = new Vector3();
	private bool isFiringPrimaries;

	// Use this for initialization
	void Start () {
	}



	void FixedUpdate()
	{
		Vector3 moveVec = new Vector3 (Input.GetAxis ("Horizontal"), 0, Input.GetAxis ("Vertical"));
		if(mainBody.transportSocket.transport != null)
			mainBody.transportSocket.transport.nodeObject.Drive(moveVec);

		playerCamera.transform.position = mainBody.transform.position + cameraOffset;
		playerCamera.transform.rotation = Quaternion.Euler (cameraAngle);

		if ((Input.GetJoystickNames ().Length == 0) || !Settings.bUsingController) {
			gunAimVec = Input.mousePosition - playerCamera.WorldToScreenPoint (mainBody.transform.position);
		}
		gunAimVec.Set (gunAimVec.x, 0, gunAimVec.y);
		mainBody.transform.rotation = Quaternion.Euler (0, Quaternion.FromToRotation (new Vector3 (0, 0, 1), gunAimVec).eulerAngles.y, 0);
	}

	void Update () {		 
		if (Input.GetButtonDown ("Fire1")) {
			mainBody.StartFirePrimary ();
		}

		if (Input.GetButtonUp ("Fire1")) {
			mainBody.CeaseFirePrimary ();
		}
	
	}
}
