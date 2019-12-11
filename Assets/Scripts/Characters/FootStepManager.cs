using UnityEngine;
using System.Collections;

public class FootStepManager : MonoBehaviour
{
	private PlayerAudioManager pam;
	private void Awake()
	{
		pam = transform.parent.GetComponentInChildren<PlayerAudioManager>();
	}

	public void FootStepEvent()
	{
		pam.AudioFootsteps(0);
	}
}
