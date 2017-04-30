using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugTools
{
	[MenuItem("Debug/Reset User", false, 0)]
	public static void ResetUser()
	{
		PlayerPrefs.DeleteAll();
	}
}
