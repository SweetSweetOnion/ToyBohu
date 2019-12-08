using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.VFX;

public class FXManager : MonoBehaviour
{
	[SerializeField]
	private VisualEffect hitConfirm;
	[SerializeField]
	private VisualEffect parry;
	[SerializeField]
	private VisualEffect dash;


	public void HitFX()
	{
		if (!hitConfirm) return;
		hitConfirm.transform.position = transform.position;
		hitConfirm.SendEvent("OnPlay");
	}

	public void ParryFX()
	{
		if (!parry) return;
		parry.transform.position = transform.position;
		parry.SendEvent("OnPlay");
	}

	public void DashFx()
	{
		if (!dash) return;
	}
}
