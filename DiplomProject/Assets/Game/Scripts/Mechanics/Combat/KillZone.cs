using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class KillZone : MonoBehaviour
{
	public List<string> TargetTags = new List<string>();
	public Action<Collider2D> CollisionHandler;
	public bool IsActive;
	private List<Collider2D> targets = new List<Collider2D>();

	public void SetValues(List<string> targetTags, Action<Collider2D> collisionHandler)
	{
		this.TargetTags = targetTags;
		this.CollisionHandler = collisionHandler;
	}

	private void Start()
	{
	}

	public void Activate()
	{
		foreach (var target in targets) {
			if (TargetTags.Contains(target.tag)) {
				CollisionHandler.Invoke(target);
			}
		}
	}

	protected void OnTriggerEnter2D(Collider2D collision)
	{
		targets.Add(collision);
	}

	protected void OnTriggerExit2D(Collider2D collision)
	{
		targets.Remove(collision);
	}
}
