using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	public float Speed;
	private Rigidbody2D rb;

	private Vector2 velocity;
	void Start()
	{
		rb = GetComponent<Rigidbody2D>();
	}

	void Update()
	{
		UpdatePosition();
		UpdateRotation();
	}

	void UpdatePosition()
	{
		var input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
		velocity = input.normalized * Speed;
		rb.MovePosition(rb.position + velocity * Time.deltaTime);
	}

	void UpdateRotation()
	{
		var mousePos = Input.mousePosition;
		var mouseWorldPos = Camera.main.ScreenToWorldPoint(mousePos);
		var dir = new Vector2(mouseWorldPos.x - transform.position.x,mouseWorldPos.y - transform.position.y);
		transform.up = dir;
	}
}
