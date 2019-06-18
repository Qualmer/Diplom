﻿using UnityEngine;

public abstract class ItemToPickUp : Interactable
{
	public Item Item;

	public override void Interact()
	{
		base.Interact();
		PickUp();
	}

	public virtual void PickUp()
	{
		Debug.Log($"Подмаем {gameObject.name}");
		bool wasPickedUp = Inventory.instance.Add(Item);
	}
}
