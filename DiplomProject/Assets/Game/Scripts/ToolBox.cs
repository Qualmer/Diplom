using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public static class ToolBox
{
	public static List<T> ListOfEnum<T>()
	{
		if (!typeof(T).IsEnum) {
			throw new Exception($"{typeof(T).Name} ето вам не ето");
		}

		return new List<T>((T[])Enum.GetValues(typeof(T)));
	}
}
