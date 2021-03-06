﻿using System.Collections;
using UnityEngine;

public enum SocketSide
{
	Left,
	Right,
	Top
}


[System.Serializable]
public class PrimaryWeaponSocket
{
	public PrimaryWeaponNode primaryWeapon;
	public Vector3 offset;
	public SocketSide side;
}


[System.Serializable]
public class TransportSocket
{
	public TransportNode transport;
	public Vector3 offset;
}

[DisallowMultipleComponent()]
[RequireComponent(typeof(CharacterController))]
public class BodySockets : MonoBehaviour
{
	public PrimaryWeaponSocket[] primaryWeaponSockets;
	public TransportSocket transportSocket;
	private CharacterController mainBody;

	private bool _isFiring;
	public bool isFiring
	{
		get
		{
			return _isFiring;
		}
	}


	void Start()
	{
		mainBody = GetComponent<CharacterController> ();
		for (int i = 0; i < primaryWeaponSockets.Length; i++) {
			PrimaryWeaponNode pwn;
			if ((pwn = primaryWeaponSockets [i].primaryWeapon) != null) {
				AttachPrimaryWeapon (i, Instantiate<PrimaryWeaponNode> (pwn));
			}
		}

		TransportNode trn;
		if ((trn = transportSocket.transport) != null) {
			AttachTransport (Instantiate<TransportNode> (trn));
		}
	}


	void Update()
	{
		if(transportSocket.transport != null)
		{
			transportSocket.transport.transform.position = transform.TransformPoint(transportSocket.offset) + transportSocket.transport.transform.rotation * Vector3.Scale(-transportSocket.transport.offset, transportSocket.transport.transform.lossyScale);
		}
	}


	public void AttachTransport(TransportNode transport)
	{
		transport.SendMessage ("OnAttach", SendMessageOptions.DontRequireReceiver);
		transportSocket.transport = transport;
		transport.gameObject.layer = gameObject.layer;
		transport.nodeObject.mainBody = mainBody;
		transport.isAttached = true;
	}

	public void DetachTransport(bool discard)
	{
		if (transportSocket.transport) {
			transportSocket.transport.SendMessage ("OnDetach", SendMessageOptions.DontRequireReceiver);
			if (!discard) {
				transportSocket.transport.isAttached = false;
				transportSocket.transport.gameObject.layer = LayerMask.NameToLayer ("Item");
				transportSocket.transport.nodeObject.mainBody = null;
				transportSocket.transport.nodeObject.StartCoroutine ("Despawn");
			} else {
				if(transportSocket.transport.gameObject != null)
					Destroy (transportSocket.transport.nodeObject.gameObject);
			}

			transportSocket.transport = null;
		}
	}
		

	public void AttachPrimaryWeapon (int index, PrimaryWeaponNode weapon)
	{
		weapon.isAttached = true;
		weapon.gameObject.layer = gameObject.layer;
		if (primaryWeaponSockets [index].side == SocketSide.Right) {
			weapon.transform.localScale = Vector3.Scale (weapon.transform.localScale, new Vector3 (-1, 1, 1));
		}
		weapon.transform.rotation = transform.rotation * weapon.transform.localRotation;
		weapon.transform.position = transform.TransformPoint(primaryWeaponSockets [index].offset) + weapon.transform.rotation * Vector3.Scale(-weapon.offset, weapon.transform.lossyScale);
		weapon.transform.SetParent (transform);
		weapon.SendMessage ("OnAttach", SendMessageOptions.DontRequireReceiver);
		primaryWeaponSockets [index].primaryWeapon = weapon;
	}

	public void DetachPrimaryWeapon(int index, bool discard)
	{
		if (primaryWeaponSockets [index].primaryWeapon) {
			primaryWeaponSockets[index].primaryWeapon.SendMessage ("OnDetach", SendMessageOptions.DontRequireReceiver);
			if (!discard) {
				primaryWeaponSockets [index].primaryWeapon.isAttached = false;
				primaryWeaponSockets [index].primaryWeapon.gameObject.layer = LayerMask.NameToLayer ("Item");
				primaryWeaponSockets [index].primaryWeapon.transform.position -= primaryWeaponSockets [index].offset;
				primaryWeaponSockets [index].primaryWeapon.transform.parent = null;
				primaryWeaponSockets [index].primaryWeapon.nodeObject.enabled = true;
				primaryWeaponSockets [index].primaryWeapon.nodeObject.StartCoroutine ("Despawn");
			} else {
				Destroy (primaryWeaponSockets [index].primaryWeapon.nodeObject.gameObject);
			}
			primaryWeaponSockets [index].primaryWeapon = null;
		}
	}


	void Die()
	{
		CeaseFirePrimary ();
	}

	public void StartFirePrimary()
	{
		foreach (var item in primaryWeaponSockets) {
			if(item.primaryWeapon != null)
			item.primaryWeapon.nodeObject.isFiring = true;
		}
		_isFiring = true;
	}

	public void CeaseFirePrimary()
	{
		foreach (var item in primaryWeaponSockets) {
			if(item.primaryWeapon != null)
			item.primaryWeapon.nodeObject.isFiring = false;
		}
		_isFiring = false;
	}


	void OnDestroy()
	{
		DetachTransport (true);
		for (int i = 0; i < primaryWeaponSockets.Length; i++) {
			DetachPrimaryWeapon (i, true);
		}
	}

	void OnDrawGizmos()
	{
		if (primaryWeaponSockets == null)
			return;
		if (primaryWeaponSockets.Length != 0) {
			Gizmos.color = Color.magenta;

			foreach (var item in primaryWeaponSockets) {
				Gizmos.DrawSphere (transform.TransformPoint (item.offset), 0.2f);
			}
		}

		Gizmos.color = Color.green;
		Gizmos.DrawSphere (transform.TransformPoint(transportSocket.offset), 0.2f);
	}
}
