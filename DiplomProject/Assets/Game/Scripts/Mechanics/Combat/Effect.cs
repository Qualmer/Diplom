using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "New Effect", menuName = "Spell/Effect")]
public class Effect : ScriptableObject
{
	[HideInInspector]
	public static Dictionary<Action, Func<float, float, float>> ActionsDictionary =
		new Dictionary<Action, Func<float, float, float>>() {
			{
				Action.Restore,
				(value, valueDelta) => {
					value += valueDelta;
					return value;
				}
			},
			{
				Action.Reduce,
				(value, valueDelta) => {
					value -= valueDelta;
					return value;
				}
			},
			{
				Action.Multiply,
				(value, valueDelta) => {
					value *= valueDelta;
					return value;
				}
			},
			{
				Action.Divide,
				(value, valueDelta) => {
					value /= valueDelta;
					return value;
				}
			},
			{
				Action.Maintenance,
				(value, valueDelta) => {
					value = valueDelta;
					return value;
				}
			}
		};
	

	public Action Action;
	public TargetField TargetField;
	public float ValueDelta;
	public float ValueDeltaDeltaPerTick;
	public int BaseTicksCount = 1;
	public int CurrentTicksCount = 1;
	public float Periodicity;
	public float PeriodicityDeltaPerTick;

	public float Activate(float value)
	{
		return ActionsDictionary[Action].Invoke(value, ValueDelta);
	}
}

public enum Action
{
	Restore,
	Reduce,
	Multiply,
	Divide,
	Maintenance
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
