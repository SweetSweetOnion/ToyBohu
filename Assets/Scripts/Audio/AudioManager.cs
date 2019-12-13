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

	[FMODUnity.EventRef]
	public string LooseEvent;
	[FMODUnity.EventRef]
	public string Toy_bohuEvent;
	[FMODUnity.EventRef]
	public string PurpleEvent;
	[FMODUnity.EventRef]
	public string PurpleWinsEvent;
	/*[FMODUnity.EventRef]
	public string Round1Event;
	[FMODUnity.EventRef]
	public string Round2Event;
	[FMODUnity.EventRef]
	public string Round3Event;*/
	[FMODUnity.EventRef]
	public string WinEvent;
	[FMODUnity.EventRef]
	public string YellowEvent;
	[FMODUnity.EventRef]
	public string YellowWinsEvent;

	private void Start()
	{
		musicInstance = RuntimeManager.CreateInstance(musicEvent);
		musicInstance.start();
		RuntimeManager.PlayOneShot(Ambiance_ExterieurEvent);
	}

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

	/*public void Round1VoixAudio()
	{
		RuntimeManager.PlayOneShot(Round1Event);
	}

	public void Round2VoixAudio()
	{
		RuntimeManager.PlayOneShot(Round2Event);
	}

	public void Round3VoixAudio()
	{
		RuntimeManager.PlayOneShot(Round3Event);
	}*/
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


	

    private void OnDestroy()
    {
        musicInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    }

    public void Tutoriel()
	{
		musicInstance.setParameterByName("Rounds", 0);
		musicInstance.setParameterByName("To Tutoriel", 0);
	}

	/*public void InitRound1()
	{
		Debug.Log("yooo");
		var t =  musicInstance.setParameterByName("Rounds", 1);
		Debug.Log(t.ToString());
	}*/

	public void Round1Audio()
    {
        RuntimeManager.PlayOneShot(Ambiance_BaseEvent);
		musicInstance.setParameterByName("Rounds", 1);
		musicInstance.setParameterByName("To Tutoriel", 0);
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
