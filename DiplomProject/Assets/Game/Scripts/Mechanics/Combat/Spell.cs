using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public abstract class Spell : MonoBehaviour
{
	public class Effect
	{
		public Effect(Action<float> action, float duration)
		{
			this.action = action;
			this.duration = duration;
		}

		public Action<float> action;
		public float duration;
	}

	public int ManaCost;
	public float Cooldown;
	private float currentCooldown;
	protected Dictionary<TargetField, Effect> Effects = new Dictionary<TargetField, Effect>();

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
