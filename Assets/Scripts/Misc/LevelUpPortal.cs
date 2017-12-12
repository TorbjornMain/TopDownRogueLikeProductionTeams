using UnityEngine;
using System.Collections;

public class LevelUpPortal : MonoBehaviour {
	[System.NonSerialized]
	public LevelManager levelManager;
    bool transition = false;
	void OnTriggerEnter(Collider other)
	{
        if(!transition)
        {
            transition = true;
            levelManager.transitionLevel();
        }
	}
}
