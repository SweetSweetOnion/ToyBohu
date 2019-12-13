using UnityEngine;
using System.Collections;
using FMODUnity;

public class LaGiraffeQuiFaitPouet : MonoBehaviour
{
	[SerializeField, FMODUnity.EventRef]
	private string pouet;

	private void OnTriggerEnter(Collider other)
	{
		RuntimeManager.PlayOneShot(pouet, transform.position);
	}
}
