
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.VFX;

[RequireComponent(typeof(Fighter))]
public class StickFX : MonoBehaviour
{
	[SerializeField]
	private VisualEffect vfx;
	[SerializeField]
	private Light fxLight;
	[SerializeField]
	private TrailRenderer[] attackTrails;
	[SerializeField]
	private Material stickMatBase;
	[SerializeField]
	private Material stickMatLight;

	private Fighter fighter;
	private MeshRenderer rend;



	private void Awake()
	{
		fighter = GetComponent<Fighter>();
		rend = fxLight.transform.parent.GetComponent<MeshRenderer>();
	}

	private void Start()
	{
		fxLight.enabled = false;
		vfx.SendEvent("OnStop");
		EndTrails();
		rend.material = stickMatBase;

	}

	private void Update()
	{
		UpdateFX();	
	}



	private void UpdateFX()
	{
		switch (fighter.currentState)
		{
			case FighterState.SetUpAttack:
				vfx.SendEvent("OnTrigger");
				rend.material = stickMatLight;
				fxLight.enabled = true;
				StartTrails();
				break;
			case FighterState.Block:
				break;
			case FighterState.Attack:
				break;
			case FighterState.AttackLag:
				EndTrails();
				break;
			default:
				EndTrails();
				fxLight.enabled = false;
				vfx.SendEvent("OnStop");
				rend.material = stickMatBase;
				break;
		}
	}

	private void StartTrails()
	{
		foreach(TrailRenderer t in attackTrails)
		{
			t.emitting = true;
		}
	}

	private void EndTrails()
	{
		foreach (TrailRenderer t in attackTrails)
		{
			t.emitting = false;
		}
	}
}
