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
    private string hit_bodyEvent;
    [FMODUnity.EventRef, SerializeField]
    private string epee_out_fourreauEvent;
    [FMODUnity.EventRef, SerializeField]
    private string epee_in_fourreauEvent;
    [FMODUnity.EventRef, SerializeField]
    private string footstepsEvent;
	private FMOD.Studio.EventInstance footInstance;

	[FMODUnity.EventRef, SerializeField]
	private string respirationFighterEvent;
	[FMODUnity.EventRef, SerializeField]
	private string voix_attaqueEvent;
	[FMODUnity.EventRef , SerializeField]
	private string voix_defenseEvent;
	[FMODUnity.EventRef , SerializeField]
	private string voix_mortEvent;

	private FMOD.Studio.EventInstance respiration;

	


	private void Start()
	{
		footInstance = RuntimeManager.CreateInstance(footstepsEvent);
		//footInstance.start();
		respiration = RuntimeManager.CreateInstance(respirationFighterEvent);
	}


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
        RuntimeManager.PlayOneShot(hit_bodyEvent);
    }
    public void AudioEpee_Out()
    {
        RuntimeManager.PlayOneShot(epee_out_fourreauEvent);
    }
    public void AudioEpee_in()
    {
        RuntimeManager.PlayOneShot(epee_in_fourreauEvent);
    }
	
    public void AudioFootsteps(int audioMaterial)
    {
		footInstance = RuntimeManager.CreateInstance(footstepsEvent);
		footInstance.setParameterByName("Tapis", audioMaterial);
		RuntimeManager.AttachInstanceToGameObject(footInstance, transform, null as Rigidbody);
		footInstance.start();
		//RuntimeManager.PlayOneShot(footstepsEvent);
    }

	public void AudioRespiration(bool b)
	{
		//TODO
		/*if (b)
		{
			bool p = false;
			respiration.getPaused(out p);
			if (p)
			{
				respiration.start();
				Debug.Log("Start");
			}
		}
		else
		{
			respiration.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
		}*/
		//RuntimeManager.PlayOneShot(respirationFighterEvent);
	}

	public void AudioVoixAttaque()
	{
		RuntimeManager.PlayOneShot(voix_attaqueEvent);
	}

	public void AudioVoixDefense()
	{
		RuntimeManager.PlayOneShot(voix_defenseEvent);
	}

	public void AudioVoixMort()
	{
		RuntimeManager.PlayOneShot(voix_mortEvent);
	}
}

