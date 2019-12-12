using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{
    private static GameObject carPrefab;
    private static float distance = 10f;


    private float speed = 20f;
    private float counter = 0f;
    public float direction = 0f;

	private void OnEnable()
	{
		AudioFx.AudioCar();
	}

	void Update()
    {
        counter += Time.deltaTime;
        transform.position += new Vector3(Time.deltaTime * speed * -direction,0f,0f);
        if(counter > 1f)
        {
            gameObject.SetActive(false);
        }
    }

    public void SpawnCar()
    {
        counter = 0.0f;
        direction = 1f;
        if (Random.value >= 0.5f)
        {
            direction = -1f;
        }
        transform.position = new Vector3(distance * direction, 0f, 0f);
    }
}
