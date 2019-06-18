using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Potion/HealPotion")]
public class HealPotion : Consumable
{
	public int HealthGain;
	public float Duration;
	public float HealPeriod;

	void Start()
    {
	}

	public override void Use()
	{
		base.Use();
	}

	void Update()
    {
        
    }
}
