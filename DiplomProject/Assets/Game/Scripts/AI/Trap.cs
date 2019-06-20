using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : Enemy
{
	protected override void UpdateRotation()
	{
		Attack();
	}

	protected override void UpdateHealthBar()
	{

	}

	public override void AddEffects(List<Effect> effects)
	{

	}
}
