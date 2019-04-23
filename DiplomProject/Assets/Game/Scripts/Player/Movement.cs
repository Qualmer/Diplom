using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
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
		var input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
		velocity = input.normalized * Speed;
		rb.MovePosition(rb.position + velocity * Time.deltaTime);
    }
}
