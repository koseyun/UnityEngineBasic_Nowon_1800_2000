using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

public class Projectile : MonoBehaviour
{
    protected bool IsGuided; // ???? ?ɼ?
    protected float Speed;
    protected float Damage;
    protected LayerMask TargetLayer;
    [SerializeField] protected LayerMask TouchLayer;
    protected Transform Target;

    public void SetUp(bool isGuided, float speed, float damage, LayerMask targetLayer, Transform target)
    {
        IsGuided = isGuided;
        Speed = speed;
        Damage = damage;
        TargetLayer = targetLayer;
        Target = target;

        transform.LookAt(target);
    }

    private void FixedUpdate()
    {
        if (Target.gameObject.activeSelf == false)
            ObjectPool.Instance.Return(gameObject);

        Move();
    }

    private void Move()
    {
        if (IsGuided)
            transform.LookAt(Target);

        transform.Translate(Vector3.forward * Speed * Time.fixedDeltaTime, Space.Self);
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        if ((1<<other.gameObject.layer & TargetLayer) > 0)
        {
            // todo -> damage to target
        }
        else if ((1<<other.gameObject.layer & TouchLayer) > 0)
        {
            // todo -> explode self
        }
    }
}
