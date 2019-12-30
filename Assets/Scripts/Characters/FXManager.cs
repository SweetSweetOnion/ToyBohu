using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.VFX;
using UnityEngine.InputSystem;

public class FXManager : MonoBehaviour
{
	[SerializeField]
	private VisualEffect hitConfirm;
	[SerializeField]
	private VisualEffect parry;
	[SerializeField]
	private VisualEffect dash;
	[SerializeField]
	private TrailRenderer[] trails;

	private void Start()
	{
		EndDashFx();
	}

	public void HitFX()
	{
		if (!hitConfirm) return;
		hitConfirm.transform.position = transform.position;
		hitConfirm.SendEvent("OnTrigger");
	}

	public void ParryFX()
	{
		if (!parry) return;
		parry.transform.position = transform.position;
		parry.SendEvent("OnTrigger");
	}

	public void DashFx()
	{
		if (!dash) return;
		dash.SendEvent("OnTrigger");
		SetTrail(true);
	}

	public void EndDashFx()
	{
		SetTrail(false);
	}

	private void SetTrail(bool active)
	{

		foreach (TrailRenderer t in trails)
		{
			t.emitting = active;
		}
	}
}
