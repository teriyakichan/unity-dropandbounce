using UnityEngine;

public class Rotate : MonoBehaviour
{
	public float speed = 10f;
	private static Vector3 _rotateSpeed;

	void Start()
	{
		_rotateSpeed = new Vector3(0f, speed);
	}
	// Update is called once per frame
	void Update()
	{
		transform.localEulerAngles += _rotateSpeed;
	}
}
