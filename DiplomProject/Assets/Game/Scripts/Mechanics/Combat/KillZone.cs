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

	public void SetValues(List<string> targetTags, Action<Collider2D> collisionHandler)
	{
		this.TargetTags = targetTags;
		this.CollisionHandler = collisionHandler;
	}

	public void Activate(float delay)
	{
		StartCoroutine(WaitForDelayAndDeactivateTask(delay));
	}

	protected void OnTriggerStay2D(Collider2D collision)
	{
		if (IsActive && TargetTags.Contains(collision.name)) {
			CollisionHandler.Invoke(collision);
		}
	}

	private IEnumerator<object> WaitForDelayAndDeactivateTask(float delay)
	{
		IsActive = true;
		yield return delay;
		IsActive = false;
	}
}
