using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "FighterData", menuName = "ToyBohu/fighterData", order = 0)]
public class FighterData : ScriptableObject
{
	[SerializeField]
	private int _maxHP = 3;
	public int maxHP => _maxHP;
	[SerializeField]
	private DashData _dash;
	public DashData dash => _dash;
	[SerializeField]
	private MovementData _movement;
	public MovementData move => _movement;
	[SerializeField]
	private AttackData _attack;
	public AttackData attack => _attack;

	[Serializable]
	public class DashData
	{
		public float coolDown = 1f;
		public float duration = 0.2f;
		public float distance = 4f;
		public AnimationCurve curve = new AnimationCurve(new Keyframe(0, 0), new Keyframe(1, 1));
	}

	[Serializable]
	public class MovementData
	{
		public float speed = 0.5f;
		public float rotationLerp = 50;
		public float microDashSpeed = 2f;
		public float microDashDuration = 0.2f;
	}

	[Serializable]
	public class AttackData
	{
		public float setUpDuration = 0.1f;
		public float blockDuration = 0.2f;
		public float attackDuration = 0.2f;
		public float lagDuration = 0.2f;
	}

}
