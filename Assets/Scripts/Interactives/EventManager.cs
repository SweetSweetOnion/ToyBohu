﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public static EventManager _instance;
    public static EventManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<EventManager>();
                if (_instance == null)
                {
                    GameObject container = new GameObject("EventManager");
                    _instance = container.AddComponent<EventManager>();
                }
            }
            return _instance;
        }
    }

    private float counter = 0f;
    private float carSpawnCooldown = 5f;

    [Header("Instances")]
    [SerializeField] private GameObject[] cars;
    [SerializeField] private GameObject lightning;
    [SerializeField] private GameObject veilleuse;
    [SerializeField] private Veilleuse scriptVeilleuse;

    private void Update()
    {
        counter += Time.deltaTime;
        if (counter > carSpawnCooldown)
        {
            
            counter = 0f;
            if (Random.value > 0.75f)
            {
				SpawnCar(0);
                if(Random.value > 0.5f)
                {
                    StartCoroutine("SpawnCarAfterABit");
                }
            }
            else if(Random.value > 0.5f)
            {
                SpawnLightning();
                StartCoroutine("SpawnLightningAfterABit");
            }
            else if(Random.value > 0.5f && TVFlicker.instance != null)
            {
                TVFlicker.instance.Trigger();
            }
            else
            {
                if (scriptVeilleuse != null && veilleuse != null) {
                    StartCoroutine("Veilleuse");
                }
            }
        }

	}

    private IEnumerator Veilleuse()
    {
        scriptVeilleuse.ActivateLight();
        veilleuse.SetActive(true);
        float rand = Random.Range(3f, 5f);
        yield return new WaitForSeconds(rand);
        veilleuse.SetActive(false);
        scriptVeilleuse.DeactivateLight();
    }

    private void SpawnCar(int i)
    {
        cars[i].SetActive(true);
        cars[i].GetComponent<Car>().SpawnCar();
    }

    private void SpawnLightning()
    {
        lightning.SetActive(true);
        lightning.GetComponent<Lightning>().SpawnLightning();
    }

	private IEnumerator SpawnCarAfterABit()
    {
		float rand = Random.Range(0.4f, 1.5f);
        yield return new WaitForSeconds(rand);
        SpawnCar(1);
    }

    private IEnumerator SpawnLightningAfterABit()
    {
        float rand = Random.Range(0.3f, 1.5f);
        yield return new WaitForSeconds(rand);
        SpawnLightning();
        if(Random.value > 0.8f)
        {
            rand = Random.Range(0.18f, 1.1f);
            yield return new WaitForSeconds(rand);
            SpawnLightning();
        }
    }

	
}
