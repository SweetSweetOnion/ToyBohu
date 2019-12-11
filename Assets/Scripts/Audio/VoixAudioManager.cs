using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class VoixAudioManager : MonoBehaviour
{
    [FMODUnity.EventRef]
    public string RespirationFighter1Event;
    [FMODUnity.EventRef]
    public string RespirationFighter2Event;
    [FMODUnity.EventRef]
    public string Voix_attaque1Event;
    [FMODUnity.EventRef]
    public string Voix_attaque2Event;
    [FMODUnity.EventRef]
    public string Voix_defense1Event;
    [FMODUnity.EventRef]
    public string Voix_defense2Event;
    [FMODUnity.EventRef]
    public string Voix_mort1Event;
    [FMODUnity.EventRef]
    public string Voix_mort2Event;

    private void Respiration1Audio()
    {
        RuntimeManager.PlayOneShot(RespirationFighter1Event);

    }


    public void Respiration2Audio()
    {
        RuntimeManager.PlayOneShot(RespirationFighter2Event);
    }

    public void Voix_attaque1Audio()
    {
        RuntimeManager.PlayOneShot(Voix_attaque1Event);

    }

    public void Voix_attaque2Audio()
    {
        RuntimeManager.PlayOneShot(Voix_attaque2Event);
    }

    public void Voix_defense1Audio()
    {
        RuntimeManager.PlayOneShot(Voix_defense1Event);
    }

    public void Voix_defense2Audio()
    {
        RuntimeManager.PlayOneShot(Voix_defense2Event);

    }

    public void Voix_mort1Audio()
    {
        RuntimeManager.PlayOneShot(Voix_mort1Event);
    }
    public void Voix_mort2Audio()
    {
        RuntimeManager.PlayOneShot(Voix_mort2Event);
    }

}
