using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Equipment")]
public abstract class Equipment : Item {

	public EquipmentSlot equipSlot;
	public int armorModifier;
	public int damageModifier;
	public SkinnedMeshRenderer prefab;

	public override void Use ()
	{
		EquipmentManager.instance.Equip(this);
		RemoveFromInventory();
	}

}

public enum EquipmentSlot {
	Head,
	Chest,
	Legs,
	Feet
}
