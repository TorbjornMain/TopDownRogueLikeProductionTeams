using System.Collections;
using UnityEngine;



[System.Serializable]
public class GunStats
{
	public string name;
	public float damage;
	public float spread;
	public int numProjectiles;
	public float speed;

	public void Zero()
	{
		name = "";
		damage = 0;
		spread = 0;
		numProjectiles = 0;
		speed = 0;
	}

	public static GunStats operator + (GunStats g1, GunStats g2)
	{
		GunStats g = new GunStats();
		g.name = g1.name + " " + g2.name;
		g.damage = Mathf.Max(0, g1.damage - 1 + g2.damage - 1);
		g.spread = Mathf.Max(0, g1.spread - 1 + g2.spread - 1);
		g.numProjectiles = Mathf.Max(0, g1.numProjectiles + g2.numProjectiles);
		g.speed = Mathf.Max(0, g1.speed + g2.speed);
		return g;
	}

	public static GunStats operator * (GunStats g1, GunStats g2)
	{
		GunStats g = new GunStats();
		g.name = g1.name + " " + g2.name;
		g.damage = g1.damage * g2.damage;
		g.spread = g1.spread * g2.spread;
		g.numProjectiles = g1.numProjectiles + g2.numProjectiles;
		g.speed = g1.speed * g2.speed;
		return g;
	}

	public static GunStats Clone(GunStats g)
	{
		GunStats r = new GunStats ();
		r.name = g.name;
		r.damage = g.damage;
		r.spread = g.spread;
		r.numProjectiles = g.numProjectiles;
		r.speed = g.speed;
		return r;
	}
}

[CreateAssetMenu()]
public class Modifier : ScriptableObject
{
	public GunStats statModifier;
}


