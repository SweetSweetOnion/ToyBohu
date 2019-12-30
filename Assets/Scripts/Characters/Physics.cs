using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CharacterController),typeof(Fighter))]
public class Physics : MonoBehaviour
{
	//inspector values
	[Header("Wind Effect")]
	[SerializeField]
	private float windStrength = 10;
	[SerializeField]
	private float windSmooth = 10;



	//private value
	private Transform animator;
	private Vector3 currentDirection;
	[SerializeField, DisplayWithoutEdit]
	private float currentSpeed;
	private Vector3 gravity = new Vector3(0, -10, 0);
	private bool isForce;
	private float forceTime = 0;
	private float windEffect = 0;

	//accessors
	public float speed => currentSpeed;
	public float velocity => controller.velocity.magnitude;
	public float orientationVelocity => Mathf.Sign( Vector3.Dot(controller.velocity.normalized, transform.forward));
	

	//external component
	private CharacterController controller;
	private FighterData fData;
	private void Awake()
	{
		controller = GetComponent<CharacterController>();
		animator = GetComponentInChildren<Animator>().transform;
		fData = GetComponent<Fighter>().data;
	}

	public void AddForce(Vector3 dir)
	{
		if (dir.magnitude < 0.01f) return;//to do check deadzone

		currentDirection = Vector3.Lerp(currentDirection, dir, fData.move.rotationLerp * Time.deltaTime);

		isForce = true;
	}

	public void directMove(Vector3 move)
	{
		controller.Move(move);
	}

    public void DirectMoveAt(Vector3 move)
    {
        controller.enabled = false;
        transform.position = move;
        controller.enabled = true;
    }

	private void Update()
	{
		if (isForce)
		{
			if(forceTime <= fData.move.microDashDuration)
			{
				currentSpeed = fData.move.microDashSpeed;
			}else
			{
				currentSpeed = fData.move.speed;
			}
			forceTime += Time.deltaTime;	
		}else
		{
			forceTime = 0;
			currentSpeed = 0;
		}
		windEffect = Mathf.Lerp(windEffect, orientationVelocity * windStrength * currentSpeed,windSmooth * Time.deltaTime);
		animator.localRotation = Quaternion.Euler(windEffect,0, 0);
		isForce = false;
		controller.Move((currentDirection * currentSpeed + gravity) * Time.deltaTime);
	}

}