using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//require area light in children

public class TVFlicker : MonoBehaviour
{
	public static TVFlicker instance;

	[SerializeField]private Material tvOn;
	[SerializeField]private Material tvOff;
	[SerializeField]private float duration = 0.7f;
	[SerializeField]private Vector2	minMaxLightIntensity = new Vector2(9,10);

	private Light areaLight;
	private MeshRenderer rend;
	private bool isRunning = false;

	private void Awake()
	{
		rend = GetComponent<MeshRenderer>();
		areaLight = GetComponentInChildren<Light>();
		instance = this;
	}


	public void Trigger()
	{
		if (!isRunning) StartCoroutine(TVRoutine());
	}

	private IEnumerator TVRoutine()
	{
		AudioFx.AudioTV();
		isRunning = true;
		rend.material = tvOn;
		float t = 0;
		while (t < duration)
		{
			areaLight.intensity = Random.Range(minMaxLightIntensity.x, minMaxLightIntensity.y);
			t += Time.deltaTime;
			yield return null;
		}
		rend.material = tvOff;
		areaLight.intensity = 0;
		isRunning = false;
	}
}
