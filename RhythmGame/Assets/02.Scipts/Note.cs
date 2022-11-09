using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum HitTypes
{
    None,
    Bad,
    Miss,
    Good,
    Great,
    Cool
}

public class Note : MonoBehaviour
{
    public void Hit(HitTypes hitTypes)
    {
        switch (hitTypes)
        {
            case HitTypes.None:
                break;
            case HitTypes.Bad:
            case HitTypes.Miss:
                GameStatus.CurrentCombo = 0;
                break;
            case HitTypes.Good:
                GameStatus.CurrentCombo++;
                break;
            case HitTypes.Great:
                GameStatus.CurrentCombo++;
                break;
            case HitTypes.Cool:
                GameStatus.CurrentCombo++;
                break;
            default:
                break;
        }

        PopUpTextManager.Instance.PopUp(hitTypes);
        Destroy(gameObject);
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        transform.position += Vector3.down * NoteSpawnManager.Instance.NoteSpeedScale * Time.fixedDeltaTime;
    }
}
