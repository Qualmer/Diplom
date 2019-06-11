using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MeeleAttack : Attack
{
	public float DurationInSeconds = .5f;
	public KillZone KillZone;
	protected virtual void Awake()
	{
		attackHandler = (collider) => {
			Debug.Log($"{collider.gameObject.name} был задет ближней атакой {gameObject.name}");
			collider.GetComponent<Unit>().AddEffects(Effects);
		};
	}

	protected virtual void Start()
	{
		Effects.Add(
			TargetField.DealPhysicalDamage,
			new Effect(
				(value) => {
					Debug.Log("Наносим 5 урончику");
					value -= 5;
				},
			0
		));
		KillZone.SetValues(
			targetTags: TargetTags,
			collisionHandler: attackHandler
		);
	}

	public override bool Cast()
	{
		if (base.Cast()) {
			KillZone.Activate();
			return true;
		}
		return false;
	}
}
