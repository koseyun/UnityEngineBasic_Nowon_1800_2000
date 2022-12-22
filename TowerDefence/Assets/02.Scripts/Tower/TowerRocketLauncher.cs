using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerRocketLauncher : TowerAttackerWithProfectile
{
    protected override void Attack()
    {
        base.Attack();

        for (int i = 0; i < FirePoints.Length; i++)
        {
            GameObject bullet = ObjectPool.Instance.Spawn("Rocket", FirePoints[i].position);
            bullet.GetComponent<ProjectileRocket>().SetUp(true, 10.0f, Damage, TargetLayer, Target);
        }
    }
}
