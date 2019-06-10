using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public abstract class Shell : MonoBehaviour
{
	public float BaseSpeed = 1;
	public List<float> SpeedModificators = new List<float>();
	public float CurrentSpeed
	{
		get {
			var value = BaseSpeed;
			foreach (var mod in SpeedModificators) {
				value += mod;
			}
			return Mathf.Clamp(value, 0, 100);
		}
	}

	protected Rigidbody2D rb;

	protected void Awake()
	{
		rb = GetComponent<Rigidbody2D>();
	}

	protected void FixedUpdate()
	{
		UpdatePosition();
		UpdateRotation();
	}

	protected virtual void UpdatePosition()
	{
		var velocity = Vector2.up * CurrentSpeed;
		rb.MovePosition(rb.position + velocity * Time.deltaTime);
	}

	protected virtual void UpdateRotation()
	{

	}
}
