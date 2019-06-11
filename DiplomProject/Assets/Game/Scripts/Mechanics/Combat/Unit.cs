using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Unit : Shell
{
	#region Health
	public int MaxHealth = 10;
	[HideInInspector]
	public int CurrentHealth { get; private set; }
	
	int SetHPPhysical {
		get {
			return CurrentHealth;
		}
		set {
			CurrentHealth -= ReduceDamage(value, 0);
		}
	}
	int SetHPMagical {
		get {
			return CurrentHealth;
		}
		set {
			CurrentHealth -= ReduceDamage(0, value);
		}
	}
	#endregion

	#region Mana
	public int MaxMana = 10;
	[HideInInspector]
	public int CurrentMana { get; private set; }
	#endregion

	#region Armor
	public int BasePhysicalArmor;
	public int BaseMagicalArmor;
	[HideInInspector]
	public int CurrentPhysicalArmor;
	[HideInInspector]
	public int CurrentMagicalArmor;
	#endregion //Armor

	public List<Spell> Spells;

	protected Dictionary<TargetField, List<Spell.Effect>> activeEffects =
		new Dictionary<TargetField, List<Spell.Effect>>();

	protected override void Awake()
	{
		base.Awake();
		CurrentHealth = MaxHealth;
		CurrentMana = MaxMana;
		CurrentPhysicalArmor = BasePhysicalArmor;
		CurrentMagicalArmor = BaseMagicalArmor;
		foreach (var targetField in ToolBox.ListOfEnum<TargetField>()) {
			activeEffects.Add(targetField, new List<Spell.Effect>());
		}
	}

	protected int ReduceDamage(int physical, int magical)
	{
		physical *= 1 - Mathf.Clamp(CurrentPhysicalArmor, -90, 90) / 100;
		magical *= 1 - Mathf.Clamp(CurrentMagicalArmor, -90, 90) / 100;
		Debug.Log($"{gameObject.name} получает {physical} физического и {magical} магического урона в лицо");
		return physical + magical;
	}

	protected virtual void Update()
	{
		foreach (var effectGroup in activeEffects) {
			switch (effectGroup.Key) {
				case TargetField.DealPhysicalDamage:
					foreach (var effect in effectGroup.Value) {
						effect.action.Invoke(SetHPPhysical);
						effect.duration -= Time.deltaTime;
						if (effect.duration <= 0) {
							effectGroup.Value.Remove(effect);
						}
					}
					break;
				case TargetField.DealMagicalDamage:
					foreach (var effect in effectGroup.Value) {
						effect.action.Invoke(SetHPMagical);
						effect.duration -= Time.deltaTime;
						if (effect.duration <= 0) {
							effectGroup.Value.Remove(effect);
						}
					}
					break;
				case TargetField.Heal:
					foreach (var effect in effectGroup.Value) {
						effect.action.Invoke(CurrentHealth);
						effect.duration -= Time.deltaTime;
						if (effect.duration <= 0) {
							effectGroup.Value.Remove(effect);
						}
					}
					break;
				case TargetField.Mana:
					foreach (var effect in effectGroup.Value) {
						effect.action.Invoke(CurrentMana);
						effect.duration -= Time.deltaTime;
						if (effect.duration <= 0) {
							effectGroup.Value.Remove(effect);
						}
					}
					break;
				case TargetField.PhysicalArmor:
					foreach (var effect in effectGroup.Value) {
						effect.action.Invoke(CurrentPhysicalArmor);
						effect.duration -= Time.deltaTime;
						if (effect.duration <= 0) {
							effectGroup.Value.Remove(effect);
						}
					}
					break;
				case TargetField.MagicalArmor:
					foreach (var effect in effectGroup.Value) {
						effect.action.Invoke(CurrentMagicalArmor);
						effect.duration -= Time.deltaTime;
						if (effect.duration <= 0) {
							effectGroup.Value.Remove(effect);
						}
					}
					break;
				case TargetField.MoveSpeed:
					foreach (var effect in effectGroup.Value) {
						effect.action.Invoke(CurrentSpeed);
						effect.duration -= Time.deltaTime;
						if (effect.duration <= 0) {
							effectGroup.Value.Remove(effect);
						}
					}
					break;
				default:
					throw new Exception($"Не туда");
			}
		}
	}

	public virtual void AddEffects(Dictionary<TargetField, Spell.Effect> effects)
	{
		foreach (var effect in effects) {
			activeEffects[effect.Key].Add(effect.Value);
			
		}
	}
}

public enum TargetField
{
	DealPhysicalDamage,
	DealMagicalDamage,
	Heal,
	Mana,
	PhysicalArmor,
	MagicalArmor,
	MoveSpeed
}
