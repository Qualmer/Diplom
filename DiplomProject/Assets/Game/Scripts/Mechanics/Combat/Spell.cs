using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using System.Linq;

public abstract class Spell : MonoBehaviour
{
	public int ManaCost;
	public float Cooldown;
	public float currentCooldown = 0;
	public Spell SubSpell;
	public List<Effect> Effects = new List<Effect>();
	public List<string> TargetTags = new List<string>();

	public virtual bool Cast()
	{
		if (currentCooldown > 0) {
			return false;
		}
		currentCooldown = Cooldown;
		return true;
	}

	protected virtual void Update()
	{
		if (currentCooldown > 0) {
			currentCooldown -= Time.deltaTime;
		}
	}
}
