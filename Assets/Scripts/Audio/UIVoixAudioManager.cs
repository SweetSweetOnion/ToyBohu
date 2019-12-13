using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class UIVoixAudioManager : MonoBehaviour
{
    [FMODUnity.EventRef]
    public string LooseEvent;
    [FMODUnity.EventRef]
    public string Toy_bohuEvent;
    [FMODUnity.EventRef]
    public string PurpleEvent;
    [FMODUnity.EventRef]
    public string PurpleWinsEvent;
    [FMODUnity.EventRef]
    public string Round1Event;
    [FMODUnity.EventRef]
    public string Round2Event;
    [FMODUnity.EventRef]
    public string Round3Event;
    [FMODUnity.EventRef]
    public string WinEvent;
    [FMODUnity.EventRef]
    public string YellowEvent;
    [FMODUnity.EventRef]
    public string YellowWinsEvent;

    public void LooseAudio()
    {
        RuntimeManager.PlayOneShot(LooseEvent);
       
    }


    public void Toy_bohuAudio()
    {
        RuntimeManager.PlayOneShot(Toy_bohuEvent);
    }

    public void PurpleAudio()
    {
        RuntimeManager.PlayOneShot(PurpleEvent);

    }

    public void PurpleWinsAudio()
    {
        RuntimeManager.PlayOneShot(PurpleWinsEvent);
    }

    public void Round1Audio()
    {
        RuntimeManager.PlayOneShot(Round1Event);
    }

    public void Round2Audio()
    {
        RuntimeManager.PlayOneShot(Round2Event);

    }

    public void Round3Audio()
    {
        RuntimeManager.PlayOneShot(Round3Event);
    }
    public void WinAudio()
    {
        RuntimeManager.PlayOneShot(WinEvent);
    }

    public void YellowAudio()
    {
        RuntimeManager.PlayOneShot(YellowEvent);
    }

    public void YellowWinsAudio()
    {
        RuntimeManager.PlayOneShot(YellowWinsEvent);

    }
}

