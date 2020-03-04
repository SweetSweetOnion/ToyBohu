using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;
using System.Linq;

[RequireComponent(typeof(PlayerInput))]
public class RumbleManager : MonoBehaviour
{
	private PlayerInput ipt;

	private float lowFreq = 0;
	private float highFreq = 0;
	private float lowBase = 0;
	private float highBase = 0;
	private Coroutine lowFreqRoutine;
	private Coroutine highFreqRoutine;

	private void Awake()
	{
		ipt = GetComponent<PlayerInput>();
	}


	private void Update()
	{
		if(ipt.playerIndex < Gamepad.all.Count)
		Gamepad.all[ipt.playerIndex].SetMotorSpeeds(lowBase + lowFreq, highBase + highFreq);
	}

	private void OnDisable()
	{
		foreach (Gamepad g in Gamepad.all) g.SetMotorSpeeds(0, 0);
	}

	private IEnumerator HighFreqRumble(float maxValue, float duration)
	{
		float t = 0;
		highFreq = maxValue;
		while (t < 1)
		{
			t += Time.unscaledDeltaTime / duration;
			highFreq = (1 - t) * (1 - t) * maxValue;
			yield return null;
		}
	}

	private IEnumerator LowFreqRumble(float maxValue, float duration)
	{
		float t = 0;
		lowFreq = maxValue;
		while (t < 1)
		{
			t += Time.unscaledDeltaTime / duration;
			lowFreq = (1 - t) * (1 - t) * maxValue;
			yield return null;
		}
	}


	//public method
	public void TriggerHighFreqRumble(float maxRumble,float rumbleDuration)
	{
		if (highFreqRoutine != null) StopCoroutine(highFreqRoutine);
		highFreqRoutine = StartCoroutine(HighFreqRumble(maxRumble, rumbleDuration));
	}

	public void TriggerLowFreqRumble(float maxRumble, float rumbleDuration)
	{
		if (lowFreqRoutine != null) StopCoroutine(lowFreqRoutine);
		lowFreqRoutine = StartCoroutine(LowFreqRumble(maxRumble, rumbleDuration));
	}

	public void SetRumbleValues(float low, float high)
	{
		highBase = high;
		lowBase = low;
	}

	//Static access
	public static void TriggerRumble(float lowFreq, float highFreq, float duration, MonoBehaviour monoReference)
	{
		monoReference.StartCoroutine(GamepadsRumble(lowFreq, highFreq, duration));
	}

	private static IEnumerator GamepadsRumble(float low,float high, float duration)
	{
		float t = 0;
		while (t < 1)
		{
			t += Time.unscaledDeltaTime / duration;
			foreach(Gamepad g in Gamepad.all)
			{
				g.SetMotorSpeeds((1 - t) * (1 - t) * low, (1 - t) * (1 - t) * high);
			}
			yield return null;
		}
	}
}
