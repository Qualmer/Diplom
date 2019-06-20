using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kust : Enemy
{
	public GameObject Crown;
	protected override void Die()
	{
		Instantiate(Crown, transform.position, Quaternion.identity);
		base.Die();
	}
}
