using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class RangeAttack : Spell
{
	public GameObject ProjectilePrefab;

	public override bool Cast()
	{
		if (base.Cast()) {
			var projectile = Instantiate(ProjectilePrefab, transform.position, transform.rotation);
			projectile.GetComponent<Projectile>().CollisionHandler = (collider) => {
				if (TargetTags.Contains(collider.tag)) {
					collider.GetComponent<Unit>().AddEffects(Effects);
				}
			};
			projectile.GetComponent<Projectile>().TargetTags.AddRange(TargetTags);
		}
		return false;
	}
}
