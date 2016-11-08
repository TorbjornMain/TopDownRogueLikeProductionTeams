using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent(typeof(Transport))]
public class TransportCardOverlayWidget :	OverlayWidget {
	Transport transport;
	TransportStatCard sc;

	protected override void Start () {
		base.Start ();
		transport = GetComponent<Transport> ();
		if ((sc = widgetInstance.GetComponent<TransportStatCard> ()) == null) {
			sc.Name.text = transport.stats.name;
			sc.Speed.text = "Speed: " + transport.stats.speed.ToString ();
			sc.Type.text = "Type: " + transport.stats.speed.ToString ();
			sc.Description.text = transport.stats.description;
		}
		OverlayManager.widgets.Remove(this);
		sc.gameObject.SetActive (false);
	}

	void OnTriggerEnter(Collider other)
	{
		if (gameObject.layer == LayerMask.NameToLayer ("Item")) {
			OverlayManager.widgets.Add (this);
			sc.gameObject.SetActive (true);
			sc.Name.text = transport.stats.name;
			sc.Speed.text = "Speed: " + transport.stats.speed.ToString ();
			sc.Type.text = "Type: " + transport.stats.speed.ToString ();
			sc.Description.text = transport.stats.description;
		}
	}

	void OnAttach()
	{
		OverlayManager.widgets.Remove (this);
		if (sc != null)
			sc.gameObject.SetActive (false);
	}

	void OnTriggerExit(Collider other)
	{
		if (gameObject.layer == LayerMask.NameToLayer ("Item")) {
			OverlayManager.widgets.Remove (this);
			sc.gameObject.SetActive (false);
		}
	}

}
//send help
