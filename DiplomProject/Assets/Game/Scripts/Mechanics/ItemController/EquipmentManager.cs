using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class EquipmentManager : MonoBehaviour {

	#region Singleton


	public static EquipmentManager instance {
		get {
			if (_instance == null) {
				_instance = FindObjectOfType<EquipmentManager> ();
			}
			return _instance;
		}
	}
	static EquipmentManager _instance;

	void Awake ()
	{
		_instance = this;
	}

	#endregion

	public Equipment[] defaultWear;

	Equipment[] currentEquipment;


	public delegate void OnEquipmentChanged(Equipment newItem, Equipment oldItem);
	public event OnEquipmentChanged onEquipmentChanged;

	Inventory inventory;

	void Start ()
	{
		inventory = Inventory.instance;

		int numSlots = System.Enum.GetNames (typeof(EquipmentSlot)).Length;
		currentEquipment = new Equipment[numSlots];

		EquipAllDefault ();
	}

	void Update() {
		if (Input.GetKeyDown (KeyCode.U)) {
			UnequipAll ();
		}
	}


	public Equipment GetEquipment(EquipmentSlot slot) {
		return currentEquipment [(int)slot];
	}

	public void Equip (Equipment newItem)
	{
		Equipment oldItem = null;

		int slotIndex = (int)newItem.equipSlot;

		if (currentEquipment[slotIndex] != null)
		{
			oldItem = currentEquipment [slotIndex];

			inventory.Add (oldItem);
	
		}

		if (onEquipmentChanged != null)
			onEquipmentChanged.Invoke(newItem, oldItem);

		currentEquipment [slotIndex] = newItem;
		Debug.Log(newItem.name + " equipped!");

	}

	void Unequip(int slotIndex) {
		if (currentEquipment[slotIndex] != null)
		{
			Equipment oldItem = currentEquipment [slotIndex];
			inventory.Add(oldItem);
				
			currentEquipment [slotIndex] = null;

			if (onEquipmentChanged != null)
				onEquipmentChanged.Invoke(null, oldItem);
			
		}

	
	}

	void UnequipAll() {
		for (int i = 0; i < currentEquipment.Length; i++) {
			Unequip (i);
		}
		EquipAllDefault ();
	}

	void EquipAllDefault() {
		foreach (Equipment e in defaultWear) {
			Equip (e);
		}
	}
}
