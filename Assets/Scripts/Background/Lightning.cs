using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class Lightning : MonoBehaviour
{
    private static GameObject lightningPrefab;
    private float counter = 0f;
    private float duration = 0.12f;

	private void Start()
	{
		
	}

	void Update()
    {
        counter += Time.deltaTime;
        if (counter > duration)
        {
            Destroy(gameObject);
        }
    }

    public static void SpawnLightning()
    {
		AudioFx.AudioLightning();
        lightningPrefab = (GameObject)Resources.Load("Prefabs/Environment/Lightning", typeof(GameObject));
        Lightning l = Object.Instantiate(lightningPrefab, new Vector3(), new Quaternion()).GetComponent<Lightning>();
        Gamefeel.Instance.InitScreenshake(0.2f,0.5f);
    }
}
