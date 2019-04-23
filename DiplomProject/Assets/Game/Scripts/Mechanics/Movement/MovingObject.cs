using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public abstract class MovingObject : MonoBehaviour
{
	public float Speed;

	private Rigidbody2D rb;
	private Vector2 velocity;
	protected void Start()
	{
		rb = GetComponent<Rigidbody2D>();
		rb.mass = 0;
		rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
		rb.interpolation = RigidbodyInterpolation2D.Interpolate;
	}

	protected void Update()
	{
		
	}

	protected void UpdatePosition()
	{
		var input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
		velocity = input.normalized * Speed;
		rb.MovePosition(rb.position + velocity * Time.deltaTime);
	}

	protected void UpdateRotation()
	{

	}
}
