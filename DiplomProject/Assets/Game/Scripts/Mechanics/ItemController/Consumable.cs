using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Consumable")]

public class Consumable : Item {

	public List<Effect> Effects;

	public override void Use()
	{
		var player = GameObject.Find("Player").GetComponent<Player>();
		player.AddEffects(Effects);
		RemoveFromInventory();
	}

}
