using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Unit : Shell
{
	#region Health
	public int MaxHealth = 10;
	[HideInInspector]
	public int CurrentHealth {
		get {
			return currentHealth;
		}
		private set {
			currentHealth = value;
			if (currentHealth <= 0) {
				Die();
			}
		}
	}
	private int currentHealth;
	
	int SetHPPhysical {
		get {
			return CurrentHealth;
		}
		set {
			CurrentHealth -= ReduceDamage(CurrentHealth - value, 0);
			Debug.Log($"Текущее здоровье {gameObject.name}: {CurrentHealth}");
		}
	}
	int SetHPMagical {
		get {
			return CurrentHealth;
		}
		set {
			CurrentHealth -= ReduceDamage(0,CurrentHealth - value);
			Debug.Log($"Текущее здоровье: {CurrentHealth}");
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
	public float CurrentPhysicalArmor;
	[HideInInspector]
	public float CurrentMagicalArmor;
	#endregion //Armor

	public Spell Spell;
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

	protected int ReduceDamage(float physical, float magical)
	{
		var delta = CurrentPhysicalArmor / 100;
		physical *= 1 - Mathf.Clamp(CurrentPhysicalArmor, -90, 90) / 100;
		magical *= 1 - Mathf.Clamp(CurrentMagicalArmor, -90, 90) / 100;
		Debug.Log($"{gameObject.name} получает {(int)physical} физического и {(int)magical} магического урона в лицо");
		return (int)(physical + magical);
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
					SetHPPhysical = (int)effect.Activate(SetHPPhysical);
				};
				break;
			case TargetField.DealMagicalDamage:
				action = () => {
					SetHPMagical = (int)effect.Activate(SetHPMagical);
				};
				break;
			case TargetField.Heal:
				action = () => {
					CurrentHealth = (int)effect.Activate(CurrentHealth);
				};
				break;
			case TargetField.Mana:
				action = () => {
					CurrentMana = (int)effect.Activate(CurrentMana);
				};
				break;
			case TargetField.PhysicalArmor:
				action = () => {
					CurrentPhysicalArmor = (int)effect.Activate(CurrentPhysicalArmor);
				};
				break;
			case TargetField.MagicalArmor:
				action = () => {
					CurrentMagicalArmor = (int)effect.Activate(CurrentMagicalArmor);
				};
				break;
			case TargetField.MoveSpeed:
				action = () => {
					CurrentSpeed = effect.Activate(CurrentSpeed);
				};
				break;
			default:
				throw new Exception($"Не туда");
		}
		while (true) {
			action.Invoke();
			yield return new WaitForSeconds(effect.Periodicity);
			effect.CurrentTicksCount --;
			if (effect.CurrentTicksCount == 0) {
				effect.CurrentTicksCount = effect.BaseTicksCount;
				effect.CurrentValueDelta = effect.BaseValueDelta;
				activeEffects.Remove(effect);
				yield break;
			}
			effect.CurrentValueDelta += effect.ValueDeltaDeltaPerTick;
		}
	}

	protected override void FixedUpdate()
	{
		base.FixedUpdate();
		UpdateHealthBar();
	}

	protected virtual void UpdateHealthBar()
	{

	}

	protected virtual void Die()
	{
		Debug.Log($"Покойся с миром {gameObject.name}");
	}
}
