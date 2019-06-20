using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Enemy : Unit
{
	Player Player;
	Vector2 playerPos;
	public Transform healthBar;
	public float attackRange;
	public float agreRange;
	float distanceToPlayer;
	Spell attackInstance;

	protected override void UpdatePosition()
	{
		if (distanceToPlayer > attackRange && distanceToPlayer < agreRange) {
			rb.MovePosition(rb.position + (Vector2)transform.up * CurrentSpeed * Time.deltaTime);
		}
	}

	protected override void UpdateRotation()
	{
		var dir = new Vector2(playerPos.x - transform.position.x, playerPos.y - transform.position.y);
		transform.up = dir;
	}

	protected override void UpdateHealthBar()
	{
		base.UpdateHealthBar();
		healthBar.SetPositionAndRotation(
			new Vector3(transform.position.x, transform.position.y + .8f, -1),
			new Quaternion(0, 0, -transform.rotation.z, 0)
		);
		healthBar.localScale = new Vector3(((float)CurrentHealth / (float)MaxHealth) * 10, .4f, 1);
	}

	protected override void FixedUpdate()
	{
		if (distanceToPlayer < attackRange) {
			Attack();
		}
		playerPos = Player.transform.position;
		distanceToPlayer = Vector2.Distance(transform.position, playerPos);
		base.FixedUpdate();
	}

	protected override void Die()
	{
		base.Die();
		Destroy(gameObject);
	}

	protected void Attack()
	{
		attackInstance.Cast();
	}

	void Start()
    {
		attackInstance = Instantiate(Spell, transform);
		Player = GameObject.Find("Player").GetComponent<Player>();
    }
}
