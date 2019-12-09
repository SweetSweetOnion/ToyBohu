using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
public class PlayerAudioManager : MonoBehaviour
{
    [FMODUnity.EventRef, SerializeField]
    private string dashEvent;
    [FMODUnity.EventRef, SerializeField]
    private string attaque1_etape1_wooshEvent;
    [FMODUnity.EventRef, SerializeField]
    private string attaque2_etape1_wooshEvent;
    [FMODUnity.EventRef, SerializeField]
    private string block_armesEvent;
    [FMODUnity.EventRef, SerializeField]
    private string Hit_bodyEvent;
    [FMODUnity.EventRef, SerializeField]
    private string Epee_out_fourreauEvent;
    [FMODUnity.EventRef, SerializeField]
    private string Epee_in_fourreauEvent;
    [FMODUnity.EventRef, SerializeField]
    private string FootstepsEvent;
    

    public void AudioDash()
    {
        RuntimeManager.PlayOneShot(dashEvent);
    }

    public void AudioAttaque1_Woosh()
    {
        RuntimeManager.PlayOneShot(attaque1_etape1_wooshEvent);
    }
    public void AudioAttaque2_Woosh()
    {
        RuntimeManager.PlayOneShot(attaque2_etape1_wooshEvent);
    }
    public void AudioBlockArmes()
    {
        RuntimeManager.PlayOneShot(block_armesEvent);
    }
    public void AudioHitBody()
    {
        RuntimeManager.PlayOneShot(Hit_bodyEvent);
    }
    public void AudioEpee_Out()
    {
        RuntimeManager.PlayOneShot(Epee_out_fourreauEvent);
    }
    public void AudioEpee_in()
    {
        RuntimeManager.PlayOneShot(Epee_in_fourreauEvent);
    }
    public void AudioFootsteps()
    {
        RuntimeManager.PlayOneShot(FootstepsEvent);
    }

}

