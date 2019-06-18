using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Projectile : Shell
{
	public Action<Collider2D> CollisionHandler;

	protected virtual void OnTriggerEnter2D(Collider2D collision)
	{
		CollisionHandler.Invoke(collision);
		Destroy(gameObject);
	}
}
