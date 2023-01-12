using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Enemy : MonoBehaviour
{
    public float Speed;

    private List<Transform> _path;
    private int _currentPathPointIndex;
    private Transform _nextPoint;
    private float _posTolerance = 0.05f;
    private Rigidbody _rb;

    private void Awake()
    {
        _rb= GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        Vector3 targetPos = new Vector3(_nextPoint.position.x,
                                        _rb.position.y,
                                        _nextPoint.position.z);
        Vector3 dir = (_rb.position - targetPos).normalized;

        // ���� ����Ʈ�� ������
        if (Vector3.Distance(targetPos, _rb.position) < _posTolerance)
        {
            if (TryGetNextPoint(_currentPathPointIndex, out _nextPoint))
            {
                _currentPathPointIndex++;
            }
            else
            {
                OnReachedToEnd();
            }

        }

        _rb.rotation = Quaternion.LookRotation(dir);
        _rb.MovePosition(_rb.position + dir * Speed * Time.fixedDeltaTime);
    }

    private bool TryGetNextPoint(int pointIndex, out Transform nextPoint)
    {
        nextPoint = null;
        
        if (pointIndex < _path.Count - 1)
        {
            nextPoint = _path[pointIndex + 1];
        }

        return nextPoint;
    }

    private void OnReachedToEnd()
    {
        // todo -> �÷��̾� ü�� ����
        // todo -> �ڱ��ڽ� �ı�
    }
}
