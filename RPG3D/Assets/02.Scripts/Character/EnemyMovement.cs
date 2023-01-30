using UnityEngine;

namespace ULB.RPG
{
    public class EnemyMovement : MovementBase
    {
        protected override float v => Input.GetAxis("Vertical");

        protected override float h => Input.GetAxis("Horizontal");

        protected override float gain => Input.GetKey(KeyCode.LeftShift) ? 1.0f : 0.5f;
    }
}
