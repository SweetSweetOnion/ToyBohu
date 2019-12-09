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

    private void Start()
    {
        RuntimeManager.PlayOneShot(Ambiance_ExterieurEvent);
        RuntimeManager.PlayOneShot(RonflementsEvent);
    }


    public void Round1Audio()
    {
        RuntimeManager.PlayOneShot(Ambiance_BaseEvent);
    }

    public void Round2Audio()
    {
        RuntimeManager.PlayOneShot(Ambiance_Round2Event);

    }

    public void Round3Audio()
    {
        RuntimeManager.PlayOneShot(Ambiance_Round3Event);
    }
}
