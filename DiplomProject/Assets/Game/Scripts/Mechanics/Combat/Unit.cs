using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : Shell
{
	#region Health
	public int BaseMaxHealth;
	[HideInInspector]
	public List<int> MaxHealthModificators = new List<int>();
	public int MaxHealth {
		get {
			return ToolBox.CalculateModificatedValue(BaseMaxHealth, MaxHealthModificators);
		}
	}
	public int CurrentHealth { get; private set; }
	#endregion

	#region Mana
	public int BaseMaxMana;
	[HideInInspector]
	public List<int> MaxManaModificators = new List<int>();
	public int MaxMana { get { return ToolBox.CalculateModificatedValue(BaseMaxMana, MaxManaModificators); } }
	public int CurrentMana { get; private set; }
	#endregion

	#region Armor
	public int BasePhysicalArmor;
	public int BaseMagicalArmor;
	public List<int> PhysicalArmorModificators;
	public List<int> MagicalArmorModificators;

	public int PhysicalArmor
	{
		get {
			return ToolBox.CalculateModificatedValue(BasePhysicalArmor, PhysicalArmorModificators);
		}
	}
	public int MagicalArmor
	{
		get {
			var value = BaseMagicalArmor;
			foreach (var mod in MagicalArmorModificators) {
				value += mod;
			}
			return value;
		}
	}
	#endregion //Armor

	protected int ReduceDamage(int physical, int magical)
	{
		physical *= 1 - Mathf.Clamp(PhysicalArmor, -90, 90) / 100;
		magical *= 1 - Mathf.Clamp(MagicalArmor, -90, 90) / 100;
		Debug.Log($"{gameObject.name} получает {physical} физического и {magical} магического урона в лицо");
		return physical + magical;
	}

	public virtual void TakeDamage(Damage d)
	{
		var damage = d.CalculateCurrentDamage();
		var reducedDamage = ReduceDamage(damage.Physical, damage.Magical);
		CurrentHealth -= reducedDamage;
	}

	
}
