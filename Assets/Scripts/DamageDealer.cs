using UnityEngine;
using System.Collections;

public struct DamageInfo 
{
	public DamageableItem target;
	public float baseDamage;
}

public class DamageDealer : MonoBehaviour {
	public float damageMultiplier = 1;

	public float DealDamage(DamageInfo dmgInfo)
	{
		dmgInfo.target.changeHealth (-dmgInfo.baseDamage * damageMultiplier);
		return -dmgInfo.baseDamage * damageMultiplier;
	}
}
