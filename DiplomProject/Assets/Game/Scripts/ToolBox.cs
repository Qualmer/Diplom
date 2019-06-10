using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ToolBox
{
	public static float CalculateModificatedValue(float value, List<float> modificators)
	{
		foreach (var mod in modificators) {
			value += mod;
		}
		return value;
	}

	public static int CalculateModificatedValue(int value, List<int> modificators)
	{
		foreach (var mod in modificators) {
			value += mod;
		}
		return value;
	}
}
