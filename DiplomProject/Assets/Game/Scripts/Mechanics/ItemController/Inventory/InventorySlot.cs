using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour {

	public Image icon;
	public Button removeButton;

	Item item;

	public void AddItem (Item newItem)
	{
		item = newItem;

		icon.sprite = item.sprite;
		icon.enabled = true;
		removeButton.interactable = true;
	}

	public void ClearSlot ()
	{
		item = null;

		icon.sprite = null;
		icon.enabled = false;
		removeButton.interactable = false;
	}

	public void RemoveItemFromInventory ()
	{
		Inventory.instance.Remove(item);
	}

	public void UseItem ()
	{
		if (item != null)
		{
			item.Use();
		}
	}

}
