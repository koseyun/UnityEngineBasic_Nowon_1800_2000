using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;

public class CursorController : SingletonMonoBase<CursorController>
{
    public void ActiveCursor()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
    }

    public void DeactiveCusor()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
}

/* �Ͻ����� �ڵ�
    public class PauseController
    {
        public static PauseController instance;
        private List<IPausable> pausables;
        public void Register(IPausable pausable)
        {
            pausables.Add(pausable);
        }

        public void PauseAll()
        {
            foreach (var pausable in pausables)
            {
                pausable.Pause(true);
            }
        }
    }

    public interface IPausable // �븮�� ���� �븮�ڿ� ����
    {
        bool isPaused { get; }
        void Pause(bool pause);
    }

    public class Monster : MonoBehaviour, IPausable
    {
        public bool isPaused => enabled;

        public void Pause(bool pause)
        {
            enabled = pause;
        }

        private void Awake()
        {
            PauseController.instance.Register(this);
        }
    }*/

/*Coroutine ���ο��� delta time �� ����ϸ� ������ ���� (�� ��û �����Ѱ� �ƴ�)
    CanvasGroup cg;
    IEnumerator E_FadeIn(float duration)
    {
        cg.alpha = 0.0f;
        float fadeDelta = 1.0f / duration;
        while (duration > 0)
        {
            cg.alpha += 1.0f / fadeDelta;
            duration -= Time.deltaTime;
            yield return null;
        }
        cg.alpha = 1.0f;
    }

    IEnumerator E_FadeInWithElapsedTime(float duration)
    {
        cg.alpha = 0.0f;
        float timeMark = Time.time;
        while (Time.time - timeMark < duration)
        {
            cg.alpha += 1.0f / duration;
            yield return null;
        }
        cg.alpha = 1.0f;
    }

    void Pause()
    {
        Time.timeScale = 0.0f;
    }
    ������ �ȵ�
    */