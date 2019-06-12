using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Weapon")]
public class Weapon : Equipment
{
	public Attack Attack1;
	public Attack Attack2;

	public override void Use()
	{
		base.Use();

	}
}
