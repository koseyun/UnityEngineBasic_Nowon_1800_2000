using JetBrains.Annotations;
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
    public const int SCORE_COOL = 500;
    public const int SCORE_GREAT = 300;
    public const int SCORE_GOOD = 100;

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
                ScoringText.Instance.Score += SCORE_GOOD;
                GameStatus.CurrentCombo++;
                break;
            case HitTypes.Great:
                ScoringText.Instance.Score += SCORE_GREAT;
                GameStatus.CurrentCombo++;
                break;
            case HitTypes.Cool:
                ScoringText.Instance.Score += SCORE_COOL;
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
