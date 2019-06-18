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
	protected List<Effect> activeEffects =
		new List<Effect>();

	protected override void Awake()
	{
		base.Awake();
		CurrentHealth = MaxHealth;
		CurrentMana = MaxMana;
		CurrentPhysicalArmor = BasePhysicalArmor;
		CurrentMagicalArmor = BaseMagicalArmor;
	}

	protected int ReduceDamage(int physical, int magical)
	{
		physical *= 1 - Mathf.Clamp(CurrentPhysicalArmor, -90, 90) / 100;
		magical *= 1 - Mathf.Clamp(CurrentMagicalArmor, -90, 90) / 100;
		Debug.Log($"{gameObject.name} получает {physical} физического и {magical} магического урона в лицо");
		return physical + magical;
	}

	public virtual void AddEffects(List<Effect> effects)
	{
		foreach (var effect in effects) {
			StartCoroutine(HandleEffectTask(effect));
		}
	}

	protected IEnumerator<object> HandleEffectTask(Effect effect)
	{
		activeEffects.Add(effect);
		System.Action action;
		switch (effect.TargetField) {
			case TargetField.DealPhysicalDamage:
				action = () => {
					effect.Activate(SetHPPhysical);
				};
				break;
			case TargetField.DealMagicalDamage:
				action = () => {
					effect.Activate(SetHPMagical);
				};
				break;
			case TargetField.Heal:
				action = () => {
					effect.Activate(CurrentHealth);
				};
				break;
			case TargetField.Mana:
				action = () => {
					effect.Activate(CurrentMana);
				};
				break;
			case TargetField.PhysicalArmor:
				action = () => {
					effect.Activate(CurrentPhysicalArmor);
				};
				break;
			case TargetField.MagicalArmor:
				action = () => {
					effect.Activate(CurrentMagicalArmor);
				};
				break;
			case TargetField.MoveSpeed:
				action = () => {
					effect.Activate(CurrentSpeed);
				};
				break;
			default:
				throw new Exception($"Не туда");
		}
		while (true) {
			yield return effect.Periodicity;
			effect.TicksCount --;
			if (effect.TicksCount == 0) {
				activeEffects.Remove(effect);
				yield break;
			}
		}
	}
}
