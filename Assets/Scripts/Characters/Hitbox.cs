using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hitbox : MonoBehaviour
{
	[SerializeField]private Light attackLight;
    [HideInInspector] public Fighter opponent;
	private bool _attacking = false;

	private void Start()
	{
		SetAttacking(false);
	}

    public bool IsAttacking()
    {
        return _attacking;
    }

    private void Update()
    {
        if (_attacking && opponent.currentHitbox.IsAttacking() && Vector3.SqrMagnitude(opponent.currentHitbox.transform.position- transform.position) < 0.5f * 0.5f)
        {
            GetComponentInParent<Fighter>().HitboxCollide();
            opponent.GetComponent<Fighter>().HitboxCollide();
        }
    }

    public void SetAttacking(bool newValue)
	{
		_attacking = newValue;
		attackLight.enabled = _attacking;
	}

	private void OnTriggerStay(Collider collider)
    {
		if (!_attacking) return;
        if (collider.gameObject != null && collider.gameObject.GetComponent<Fighter>() != null && collider.gameObject.GetComponent<Fighter>().Equals(opponent))
        {
			opponent.Damage(1);
            GetComponentInParent<Fighter>().SucceedAttack();
            return;
        }
    }
}
