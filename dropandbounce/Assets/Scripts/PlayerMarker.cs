using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMarker : MonoBehaviour
{
	private static Vector3 _rotateSpeed = new Vector3(0f, 10f);
	// Update is called once per frame
	void Update()
	{
		transform.localEulerAngles += _rotateSpeed;
	}
}
