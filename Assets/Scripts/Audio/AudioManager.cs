using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class AudioManager : MonoBehaviour
{
    [FMODUnity.EventRef]
    public string Ambiance_BaseEvent;
    [FMODUnity.EventRef]
    public string Ambiance_Round2Event;
    [FMODUnity.EventRef]
    public string Ambiance_Round3Event;
    [FMODUnity.EventRef]
    public string Ambiance_ExterieurEvent;
    [FMODUnity.EventRef]
    public string RonflementsEvent;

	[FMODUnity.EventRef]
	public string musicEvent;

	private FMOD.Studio.EventInstance musicInstance;
	

    private void Start()
    {
		musicInstance = RuntimeManager.CreateInstance(musicEvent);
		musicInstance.start();
    }

	public void Tutoriel()
	{
		musicInstance.setParameterByName("To Début Combat", 0);
		musicInstance.setParameterByName("To Tutoriel", 0);
	}

	public void InitRound1()
	{
		musicInstance.setParameterByName("To Début Combat", 1);
	}

	public void Round1Audio()
    {
        RuntimeManager.PlayOneShot(Ambiance_BaseEvent);
		musicInstance.setParameterByName("To Début Combat", 1);
		musicInstance.setParameterByName("To Tutoriel", 0);
		musicInstance.setParameterByName("Rounds", 0);
	}

    public void Round2Audio()
    {
        RuntimeManager.PlayOneShot(Ambiance_Round2Event);
		musicInstance.setParameterByName("Rounds", 2);
	}

    public void Round3Audio()
    {
        RuntimeManager.PlayOneShot(Ambiance_Round3Event);
		musicInstance.setParameterByName("Rounds", 3);
	}
}
