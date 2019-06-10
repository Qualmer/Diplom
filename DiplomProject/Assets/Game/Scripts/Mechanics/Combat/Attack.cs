using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class Attack : Spell
{
	public Damage Damage;
	public event Action<Damage> OnTargetHited;
	protected Action<Collider2D> attackHandler;
	protected List<string> TargetTags = new List<string>() {
		"Enemy"
	};
}
