using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "New Effect", menuName = "Spell/Effect")]
public class Effect : ScriptableObject
{
	[HideInInspector]
	public static Dictionary<Action, Action<float, float>> ActionsDictionary =
		new Dictionary<Action, Action<float, float>>() {
			{
				Action.Restore,
				(value, valueDelta) => {
					value += valueDelta;
				}
			},
			{
				Action.Reduce,
				(value, valueDelta) => {
					value -= valueDelta;
				}
			},
			{
				Action.Multiply,
				(value, valueDelta) => {
					value *= valueDelta;
				}
			},
			{
				Action.Divide,
				(value, valueDelta) => {
					value /= valueDelta;
				}
			},
			{
				Action.Maintenance,
				(value, valueDelta) => {
					value = valueDelta;
				}
			}
		};
	

	public Action Action;
	public TargetField TargetField;
	public float ValueDelta;
	public float ValueDeltaDeltaPerTick;
	public int TicksCount = 1;
	public float Periodicity;
	public float PeriodicityDeltaPerTick;

	public void Activate(float value)
	{
		ActionsDictionary[Action].Invoke(value, ValueDelta);
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
