using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject {

	new public string name = "New Item";	
	public Sprite sprite = null;
	public bool showInInventory = true;

	public virtual void Use ()
	{
	}

	public void RemoveFromInventory ()
	{
		Inventory.instance.Remove(this);
	}

}
