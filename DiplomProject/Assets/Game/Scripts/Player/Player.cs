using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : Unit
{
	public Weapon Weapon;
	private Vector2 velocity;
	private SpriteRenderer spriteRenderer;
	public EquipmentManager EquipmentManager;
	GameObject weaponAttackPrefab;
	GameObject currentWeaponAttack;
	Spell attack;
	public Slider HealthBar;
	public Slider ManaBar;

	protected void Start()
	{
		spriteRenderer = GetComponent<SpriteRenderer>();
		EquipmentManager.instance.onEquipmentChanged += Instance_onEquipmentChanged;
		Weapon = (Weapon)EquipmentManager.GetEquipment(EquipmentSlot.Weapon);
		spriteRenderer.sprite = Weapon.NonArmoredSprite;
		
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
			attack = currentWeaponAttack.GetComponent<Spell>();
		}
		spriteRenderer.sprite =
				EquipmentManager.GetEquipment(EquipmentSlot.Armor) == EquipmentManager.defaultWear[1] ?
				Weapon.NonArmoredSprite :
				Weapon.ArmoredSprite;
	}

	protected void Update()
	{
		if (Input.GetKey(KeyCode.Mouse1)) {
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

	protected override void UpdateHealthBar()
	{
		base.UpdateHealthBar();
		HealthBar.value = (float)CurrentHealth / (float)MaxHealth;
		ManaBar.value = (float)CurrentMana / (float)MaxMana;
	}
}
