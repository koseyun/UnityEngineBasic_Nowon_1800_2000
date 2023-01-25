using System;
using UnityEngine;

/// <summary>
/// �ִϸ������� ���� state �� ������
/// </summary>
public class AnimatorStateMonitor : StateMachineBehaviour
{
    public event Action<int> OnEnter;
    public event Action<int> OnExit;
    [SerializeField] private string _boolparamName;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        if (string.IsNullOrEmpty(_boolparamName) == false)
            animator.SetBool(_boolparamName, true);
        OnEnter?.Invoke(stateInfo.fullPathHash);
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateExit(animator, stateInfo, layerIndex);
        if (string.IsNullOrEmpty(_boolparamName) == false)
            animator.SetBool(_boolparamName, false);
        OnExit?.Invoke(stateInfo.fullPathHash);
    }
}
