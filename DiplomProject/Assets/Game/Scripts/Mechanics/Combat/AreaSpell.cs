using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


[RequireComponent(typeof(Collider2D))]
public class AreaSpell : Spell
{
	private List<Collider2D> targets = new List<Collider2D>();

	public override bool Cast()
	{
		if (base.Cast()) {
			foreach (var target in targets) {
				target.GetComponent<Unit>().AddEffects(Effects);
			}
			return true;
		}
		return false;
	}

	protected void OnTriggerEnter2D(Collider2D collision)
	{
		if (TargetTags.Contains(collision.tag)) {
			targets.Add(collision);
		}
	}

	protected void OnTriggerExit2D(Collider2D collision)
	{
		if (targets.Contains(collision)) {
			targets.Remove(collision);
		}
	}
}
