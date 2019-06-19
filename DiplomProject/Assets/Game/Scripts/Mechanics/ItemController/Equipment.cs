using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Equipment")]
public class Equipment : Item {

	public EquipmentSlot equipSlot;
	public List<Effect> Effects;

	public override void Use ()
	{
		EquipmentManager.instance.Equip(this);
		FindObjectOfType<Player>().AddEffects(Effects);
		RemoveFromInventory();
	}

}

public enum EquipmentSlot {
	Weapon,
	Armor
}
