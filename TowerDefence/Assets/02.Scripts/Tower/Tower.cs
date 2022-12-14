using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class Tower : MonoBehaviour
{
    public TowerInfo Info;
    public Node Node { get; set; }
    [SerializeField] private Transform _rotatePoint;
    [SerializeField] private float _detectRange;
    [SerializeField] protected LayerMask TargetLayer;
    protected Transform Target;

    protected virtual void FixedUpdate()
    {
        Collider[] cols = Physics.OverlapSphere(transform.position, _detectRange, TargetLayer);

        if (cols.Length > 0)
        {
            Target = cols[0].transform;
            _rotatePoint.LookAt(Target);
        }
        else
        {
            Target = null;
        }
    }

    private void OnMouseDown()
    {
        TowerUI.Instance.SetUp(this);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, _detectRange);
    }
}
