using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Projectile : Shell
{
	public Action<Collider2D> CollisionHandler;
	public List<string> TargetTags = new List<string>();

	protected virtual void OnTriggerEnter2D(Collider2D collision)
	{
		if (TargetTags.Contains(collision.tag) || collision.tag == "Wall") {
			CollisionHandler.Invoke(collision);
			Destroy(gameObject);
		}
	}
}
