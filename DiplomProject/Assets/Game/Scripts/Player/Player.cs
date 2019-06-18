using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Unit
{
	public Weapon Weapon;
	private Vector2 velocity;
	private SpriteRenderer spriteRenderer;
	public EquipmentManager EquipmentManager;
	GameObject weaponAttackPrefab;
	GameObject currentWeaponAttack;
	Spell attack;

	protected void Start()
	{
		spriteRenderer = GetComponent<SpriteRenderer>();
		EquipmentManager.instance.onEquipmentChanged += Instance_onEquipmentChanged;
		spriteRenderer.sprite = EquipmentManager.instance.GetEquipment(EquipmentSlot.Weapon).sprite;
		Weapon = (Weapon)EquipmentManager.instance.GetEquipment(EquipmentSlot.Weapon);
		weaponAttackPrefab = Weapon.Attack;
		currentWeaponAttack = Instantiate(weaponAttackPrefab, transform);
		attack = currentWeaponAttack.GetComponent<Spell>();
	}

	private void Instance_onEquipmentChanged(Equipment newItem, Equipment oldItem)
	{
		if (newItem.equipSlot == EquipmentSlot.Weapon) {
			Destroy(currentWeaponAttack);
			Weapon = (Weapon)newItem;
			weaponAttackPrefab = Weapon.Attack;
			currentWeaponAttack = Instantiate(weaponAttackPrefab, transform);
			spriteRenderer.sprite = newItem.sprite;
			attack = currentWeaponAttack.GetComponent<Spell>();
		}
	}

	protected void Update()
	{
		if (Input.GetKeyDown(KeyCode.Mouse0)) {
			attack.Cast();
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
