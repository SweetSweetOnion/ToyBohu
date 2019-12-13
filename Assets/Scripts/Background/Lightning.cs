using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class Lightning : MonoBehaviour
{
    private static GameObject lightningPrefab;
    private float counter = 0f;
    private float duration = 0.16f;

	void Update()
    {
        counter += Time.deltaTime;
        if (counter > duration)
        {
            gameObject.SetActive(false);
        }
    }

    public void SpawnLightning()
    {
        counter = 0.0f;
		AudioFx.AudioLightning();
        Gamefeel.Instance.InitScreenshake(0.2f,0.5f);
    }
}
