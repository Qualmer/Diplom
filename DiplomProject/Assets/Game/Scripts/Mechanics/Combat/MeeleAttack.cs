using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MeeleAttack : Attack
{

	public KillZone KillZone;
	protected virtual void Awake()
	{
		attackHandler = (collider) => {
			if (!Damage.IsNull) {
				Debug.Log($"{collider.gameObject.name} был задет ближней атакой {gameObject.name}");
				collider.GetComponent<Unit>()?.TakeDamage(Damage);
			}
		};
	}

	protected virtual void Start()
	{
		KillZone.SetValues(
			targetTags: new List<string>(),
			collisionHandler: attackHandler
		);
	}

	public override void Cast()
	{
		KillZone.Activate(Cooldown);
	}
}
