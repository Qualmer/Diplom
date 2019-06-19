using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public abstract class Shell : MonoBehaviour
{
	public float BaseSpeed = 1;
	[HideInInspector]
	public float CurrentSpeed;

	protected Rigidbody2D rb;

	protected virtual void Awake()
	{
		CurrentSpeed = BaseSpeed;
		rb = GetComponent<Rigidbody2D>();
	}

	protected virtual void FixedUpdate()
	{
		UpdatePosition();
		UpdateRotation();
	}

	protected virtual void UpdatePosition()
	{
		var velocity = (Vector2)transform.up * CurrentSpeed;
		rb.MovePosition(rb.position + velocity * Time.deltaTime);
	}

	protected virtual void UpdateRotation()
	{

	}
}
