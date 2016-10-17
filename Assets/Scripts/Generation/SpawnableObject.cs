﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class SpawnableObject : MonoBehaviour, System.IComparable<SpawnableObject> {

	public uint size = 1;
	public uint levelClass = 0;
	public float heightOffset = 0.5f;
	public int CompareTo(SpawnableObject other)
	{
		return levelClass.CompareTo (other.levelClass);
	}


}