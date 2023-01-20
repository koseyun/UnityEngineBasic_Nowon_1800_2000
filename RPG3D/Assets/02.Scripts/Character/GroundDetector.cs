using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundDetector : MonoBehaviour
{
    public bool isDetected;
    [SerializeField] private Vector3 _offset;
    [SerializeField] private float _range;
    [SerializeField] private LayerMask _groundLayer;

    public bool CastGround(float distance, out RaycastHit hit)
    {
        Debug.Log(Physics.SphereCast(transform.position, _range, Vector3.down, out hit, distance, _groundLayer));
        return Physics.SphereCast(transform.position, _range, Vector3.down, out hit, distance, _groundLayer);
    }

    private void FixedUpdate()
    {
        Collider[] cols = Physics.OverlapSphere(transform.position + _offset,
                                                _range,
                                                _groundLayer);

        isDetected = cols.Length > 0;
    }

    int[,] dirPattern = new int[,]
    {
        { 1, 0, -1, 0 },
        { 0, 1, 0, -1 }
    };
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position + _offset, _range);

        /*if (CastGround(0.5f, out RaycastHit hit))
        {
            Gizmos.color = Color.cyan;
            for (int i = 0; i < dirPattern.GetLength(1); i++)
            {
                Gizmos.DrawLine(transform.position + new Vector3(dirPattern[0, i], dirPattern[1, i]) * _range / 2.0f,
                                hit.point + new Vector3(dirPattern[0, i], dirPattern[1, i]) * _range / 2.0f);
            }

            Gizmos.DrawWireSphere(transform.position, _range);
            Gizmos.DrawWireSphere(hit.point, _range);
        }*/
    }
}
