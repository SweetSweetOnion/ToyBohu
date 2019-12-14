using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OursonLookAt : MonoBehaviour
{
	[SerializeField]
	private Transform neckBone;
	[SerializeField]
	private Transform target;
	[SerializeField]
	private AnimationCurve lookCurve;
	[SerializeField]
	private float duration;

	private float time = 0;
	private Quaternion baseRotation;
	// Start is called before the first frame update
	void Start()
    {
		baseRotation = neckBone.rotation;
    }

    // Update is called once per frame
    void Update()
    {
		time += Time.deltaTime;

		if(time > duration)
		{
			time = 0;
		}
		Quaternion q = Quaternion.LookRotation(target.position - neckBone.position, Vector3.up);
		neckBone.rotation = Quaternion.Slerp(baseRotation, q, lookCurve.Evaluate(time/duration));
		
    }
}
