using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hitbox : MonoBehaviour
{
    [SerializeField] private Light attackLight;
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
        CheckParry();
    }

    private bool CheckParry()
    {
        if (_attacking && opponent.currentHitbox.IsAttacking() && Vector3.SqrMagnitude(opponent.currentHitbox.transform.position - transform.position) < 1f * 1f)
        {
            GetComponentInParent<Fighter>().HitboxCollide();
            opponent.GetComponent<Fighter>().HitboxCollide();
            return true;
        }
        return false;
    }

    public void SetAttacking(bool newValue)
    {
        _attacking = newValue;
        attackLight.enabled = _attacking;
    }

    private void OnTriggerStay(Collider collider)
    {
        if (!_attacking) return;
        if (CheckParry())
        {
            return;
        }
        if (collider.gameObject != null && collider.gameObject.GetComponent<Fighter>() != null && collider.gameObject.GetComponent<Fighter>().Equals(opponent))
        {
            opponent.Damage(1);
            opponent.currentHitbox.SetAttacking(false);
            GetComponentInParent<Fighter>().SucceedAttack();
            return;
        }
    }
}
