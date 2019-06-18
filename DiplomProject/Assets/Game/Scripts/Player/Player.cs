using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Unit
{
	public Weapon Weapon;
	private Vector2 velocity;
	private SpriteRenderer spriteRenderer;
	public EquipmentManager EquipmentManager;
	GameObject currentWeaponAttack;

	protected void Start()
	{
		spriteRenderer = GetComponent<SpriteRenderer>();
		EquipmentManager.instance.onEquipmentChanged += Instance_onEquipmentChanged;
		spriteRenderer.sprite = EquipmentManager.instance.GetEquipment(EquipmentSlot.Weapon).sprite;
		Weapon = (Weapon)EquipmentManager.instance.GetEquipment(EquipmentSlot.Weapon);
		currentWeaponAttack = Weapon.Attack;
		Instantiate(currentWeaponAttack, transform);
	}

	private void Instance_onEquipmentChanged(Equipment newItem, Equipment oldItem)
	{
		if (newItem.equipSlot == EquipmentSlot.Weapon) {
			Destroy(currentWeaponAttack);
			Weapon = (Weapon)newItem;
			currentWeaponAttack = Weapon.Attack;
			Instantiate(currentWeaponAttack, transform);
			spriteRenderer.sprite = newItem.sprite;
		}
	}

	protected void Update()
	{
		if (Input.GetKey(KeyCode.Mouse0)) {
		((AreaSpell)	currentWeaponAttack.GetComponent<Spell>()).Cast();
		}
	}

	protected override void UpdatePosition()
	{
		var input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
		velocity = input.normalized * CurrentSpeed;
		rb.MovePosition(rb.position + velocity * Time.deltaTime);
	}

	protected override void UpdateRotation()
	{
		var mousePos = Input.mousePosition;
		var mouseWorldPos = Camera.main.ScreenToWorldPoint(mousePos);
		var dir = new Vector2(mouseWorldPos.x - transform.position.x, mouseWorldPos.y - transform.position.y);
		transform.up = dir;
	}
}
