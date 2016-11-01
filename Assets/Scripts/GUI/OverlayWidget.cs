using UnityEngine;
using System.Collections;

public class OverlayWidget : MonoBehaviour {


	public float heightOffset;
	public GameObject widgetPrefab;
	[System.NonSerialized]
	public GameObject widgetInstance;


	// Use this for initialization
	protected virtual void Start () {
		widgetInstance = Instantiate<GameObject> (widgetPrefab);
		OverlayManager.widgets.Add(this);
	}
	
	// Update is called once per frame
	protected virtual void Update () {

	}

	protected virtual void OnDestroy()
	{
		if(widgetInstance != null)
			Destroy (widgetInstance);
		OverlayManager.widgets.Remove(this);
	}
}
