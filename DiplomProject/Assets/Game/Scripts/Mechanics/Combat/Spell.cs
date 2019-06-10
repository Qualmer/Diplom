using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Spell : MonoBehaviour
{
	public int BaseManaCost;
	public List<int> ManaCostModificators;
	public float BaseCooldown;
	public List<int> CooldownModificators;

	public int Manacost
	{
		get {
			var value = BaseManaCost;
			foreach (var mod in ManaCostModificators) {
				value += mod;
			}
			return value;
		}
	}
	public float Cooldown
	{
		get {
			var value = BaseCooldown;
			foreach (var mod in CooldownModificators) {
				value += mod;
			}
			return value;
		}
	}

	public abstract void Cast();
}
