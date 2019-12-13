using UnityEngine;
using System.Collections;

public class LightRotation : MonoBehaviour
{
	public float speed = 1.5f;

	void Update()
	{
		transform.localRotation *= Quaternion.Euler(0, 0, speed * Time.deltaTime);
	}
}
