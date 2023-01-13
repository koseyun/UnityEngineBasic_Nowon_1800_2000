using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// FSM �� ���� ���̽� �������̽�
/// ���¸� ������ �� �ִ� ����, Ư�� �����϶� Ư�� id �� ���·� ��ȯ�ϴ� transition �� ������
/// �ش� ���¿����� ������ Uadate �� ȣ�����ִ� ���·� ����Ѵ�.
/// </summary>
namespace ULB.RPG.FSM
{
    public interface IState
    {
        int id { get; set; }
        Func<bool> canExecute { get; set; } // �� ���¸� ������ �� �ִ� ����
        List<KeyValuePair<Func<bool>, int>> transitions { get; set; } // Ư�� ���¿��� Ư�� id�� ���·� ��ȯ�ϴ� ���
        void Execute();
        void Stop();

        /// <summary>
        /// ���� ����
        /// </summary>
        /// <returns> ��ȯ�ؾ��ϴ� ���� ������ id </returns>
        int Update();
    }
}
