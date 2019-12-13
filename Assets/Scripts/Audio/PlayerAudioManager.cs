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
		//footInstance = RuntimeManager.CreateInstance(footstepsEvent);
		//RuntimeManager.AttachInstanceToGameObject(footInstance, transform, null as Rigidbody);
		//footInstance.start();
		//respiration = RuntimeManager.CreateInstance(respirationFighterEvent);
	}


	 public void AudioDash()
    {
        RuntimeManager.PlayOneShot(dashEvent,transform.position);
    }

    public void AudioAttaque1_Woosh()
    {
        RuntimeManager.PlayOneShot(attaque1_etape1_wooshEvent, transform.position);
    }
    public void AudioAttaque2_Woosh()
    {
        RuntimeManager.PlayOneShot(attaque2_etape1_wooshEvent, transform.position);
    }
    public void AudioBlockArmes()
    {
        RuntimeManager.PlayOneShot(block_armesEvent, transform.position);
    }
    public void AudioHitBody()
    {
        RuntimeManager.PlayOneShot(hit_bodyEvent, transform.position);
    }
    public void AudioEpee_Out()
    {
        RuntimeManager.PlayOneShot(epee_out_fourreauEvent, transform.position);
    }
    public void AudioEpee_in()
    {
        RuntimeManager.PlayOneShot(epee_in_fourreauEvent, transform.position);
    }
	
    public void AudioFootsteps(int audioMaterial)
    {
		/*footInstance = RuntimeManager.CreateInstance(footstepsEvent);
		footInstance.setParameterByName("Tapis", audioMaterial);
		RuntimeManager.AttachInstanceToGameObject(footInstance, transform, null as Rigidbody);
		footInstance.start();*/
		//RuntimeManager.PlayOneShot(footstepsEvent);
		RuntimeManager.PlayOneShot(footstepsEvent,transform.position);
		//footInstance.setParameterByName("Tapis", audioMaterial);
		//footInstance.start();
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
		RuntimeManager.PlayOneShot(voix_attaqueEvent, transform.position);
	}

	public void AudioVoixDefense()
	{
		RuntimeManager.PlayOneShot(voix_defenseEvent, transform.position);
	}

	public void AudioVoixMort()
	{
		RuntimeManager.PlayOneShot(voix_mortEvent, transform.position);
	}
}

