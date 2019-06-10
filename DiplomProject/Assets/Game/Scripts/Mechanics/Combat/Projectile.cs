using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class Projectile : Shell
{
	public KillZone KillZone;

	protected Action<Collider2D> CollisionHandler;
	protected List<string> TargetTags = new List<string>() {
		"Enemy"
	};

	public virtual void SetValues(
		List<string> targetTags,
		Action<Collider2D> projectileCollisionHandler,
		Action<Collider2D> killZoneCollisionHandler
	)
	{
		TargetTags = targetTags;
		CollisionHandler = projectileCollisionHandler;
		KillZone.SetValues(targetTags, killZoneCollisionHandler);
	}

	protected void OnTriggerStay2D(Collider2D collision)
	{
		if (TargetTags.Contains(collision.name)) {
			CollisionHandler.Invoke(collision);
		}
	}

	void Start()
    {
        
    }
}
