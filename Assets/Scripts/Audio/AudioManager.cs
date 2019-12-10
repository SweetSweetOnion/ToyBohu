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

	public StudioEventEmitter musicEvent;

    private void Start()
    {
		//RuntimeManager.PlayOneShot(Ambiance_ExterieurEvent);
		//RuntimeManager.PlayOneShot(RonflementsEvent);
		Round2Audio();
    }


    public void Round1Audio()
    {
        RuntimeManager.PlayOneShot(Ambiance_BaseEvent);
		musicEvent.SetParameter("Rounds", 1);
    }

    public void Round2Audio()
    {
        RuntimeManager.PlayOneShot(Ambiance_Round2Event);
		musicEvent.SetParameter("Rounds", 2);
	}

    public void Round3Audio()
    {
        RuntimeManager.PlayOneShot(Ambiance_Round3Event);
		musicEvent.SetParameter("Rounds", 3);
	}
}
