using System.Collections.Generic;
using UnityEngine;

public interface ICainBeHited
{
	/// <summary>
	/// Позволяет данному объекту получить по лицу
	/// </summary>
	void TakeDamage(Damage damage);
}

public struct Damage // Надо бы попробовать впихнуть модификаторы прямо в урон
{
	public int MinPhysicalDamage;
	public int MaxPhysicalDamage;
	private List<int> PhysicalDamageModificators;

	public int MinMagicalDamage;
	public int MaxMagicalDamage;
	private List<int> MagicalDamageModificators;

	public bool IsNull =>
		MinMagicalDamage == MaxMagicalDamage &&
		MaxMagicalDamage == MinPhysicalDamage &&
		MinPhysicalDamage == MaxPhysicalDamage &&
		MaxPhysicalDamage == 0;

	public (int Physical, int Magical) CalculateBaseDamage()
	{
		var physical = (int)Mathf.Lerp(MinPhysicalDamage, MaxPhysicalDamage, .5f);
		var magical = (int)Mathf.Lerp(MinMagicalDamage, MaxMagicalDamage, .5f);
		return (physical, magical);
	}

	public (int Physical, int Magical) CalculateRandomDamage()
	{
		var r = new System.Random();
		var physical = r.Next(MinPhysicalDamage, MaxPhysicalDamage + 1);
		var magical = r.Next(MinMagicalDamage, MaxMagicalDamage + 1);
		return (physical, magical);
	}

	public (int Physical, int Magical) CalculateCurrentDamage()
	{
		var result = CalculateRandomDamage();
		foreach (var mod in PhysicalDamageModificators) {
			result.Physical += mod;
		}
		foreach (var mod in MagicalDamageModificators) {
			result.Magical += mod;
		}
		return result;
	}

	public void AddModificator(int value, Type type)
	{
		switch (type) {
			case Type.Physical:
				PhysicalDamageModificators.Add(value);
				break;
			case Type.Magical:
				MagicalDamageModificators.Add(value);
				break;
			default:
				throw new System.Exception("Боже, Шо ето за дэмэдж!?");
		}
	}

	public enum Type
	{
		Physical,
		Magical
	}
}