using UnityEngine;
using System.Collections;
using FMODUnity;
public class AudioFx : MonoBehaviour
{
	public static AudioFx instance;

	[SerializeField, FMODUnity.EventRef]
	private string lightningAudio;
	[SerializeField, FMODUnity.EventRef]
	private string tvAudio;
	[SerializeField, FMODUnity.EventRef]
	private string carAudio;

	private void Awake()
	{
		instance = this;
	}

	public static void AudioLightning()
	{
		RuntimeManager.PlayOneShot(instance.lightningAudio);
	}

	public static void AudioCar()
	{
		RuntimeManager.PlayOneShot(instance.carAudio);
	}

	public static void AudioTV()
	{
		RuntimeManager.PlayOneShot(instance.tvAudio,FindObjectOfType<TVFlicker>().transform.position);
	}
}
