using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using System;

public class RagedSpell : Spell
{
	public GameObject ProjectilePrefab;

	protected void Start()
	{
		ProjectilePrefab.GetComponent<Projectile>().CollisionHandler += (collider) => {
			if (TargetTags.Contains(collider.tag)) {
				collider.GetComponent<Unit>().AddEffects(Effects);
			}
		};
	}

	public override bool Cast()
	{
		if (base.Cast()) {
			Instantiate(ProjectilePrefab, transform);
		}
		return false;
	}
}
