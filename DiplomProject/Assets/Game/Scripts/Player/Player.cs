using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Unit
{
	public float Speed;

	private Vector2 velocity;

	protected override void UpdatePosition()
	{
		var input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
		velocity = input.normalized * Speed;
		rb.MovePosition(rb.position + velocity * Time.deltaTime);
	}

	protected override void UpdateRotation()
	{
		var mousePos = Input.mousePosition;
		var mouseWorldPos = Camera.main.ScreenToWorldPoint(mousePos);
		var dir = new Vector2(mouseWorldPos.x - transform.position.x,mouseWorldPos.y - transform.position.y);
		transform.up = dir;
	}
}
