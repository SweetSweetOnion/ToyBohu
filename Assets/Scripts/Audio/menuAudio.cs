using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class menuAudio : MonoBehaviour
{
	[FMODUnity.EventRef]
	public string musicMenu;

	private FMOD.Studio.EventInstance menu;

	private void OnEnable()
	{
		menu = RuntimeManager.CreateInstance(musicMenu);
		menu.start();
	}

	private void OnDisable()
	{
		menu.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
	}
}
