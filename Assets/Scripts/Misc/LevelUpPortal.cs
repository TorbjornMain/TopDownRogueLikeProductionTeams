using UnityEngine;
using System.Collections;

public class LevelUpPortal : MonoBehaviour {
	[System.NonSerialized]
	public LevelManager levelManager;

	void OnTriggerEnter(Collider other)
	{
		levelManager.transitionLevel();
	}
}
