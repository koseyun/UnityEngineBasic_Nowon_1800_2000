using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class DiceAnimationUI : MonoBehaviour
{
    public static DiceAnimationUI Instance;

    [SerializeField] private Image _image;
    [SerializeField] private Button _normalDiceButton;
    [SerializeField] private Button _goldenDiceButton;

    [SerializeField] private float _animationDelay;
    [SerializeField] private float _animationTime;
    private float _animationDelayTimer;
    private List<Sprite> _sprites;

    public event Action OnAnimationStarted;
    public event Action<int> OnAnimationFinished;

    //public delegate void AnimationFinishedHandler(int diceValue);
    //public AnimationFinishedHandler OnAnimationFinished2;

    public void PlayDiceAnimation(int diceValue)
    {
        StartCoroutine(E_DiceAnimation(diceValue));
    }

    private void Awake()
    {
        Instance = this;
        LoadSprites();
        OnAnimationStarted += () =>
        {
            _normalDiceButton.interactable = false;
            _goldenDiceButton.interactable = false;
        };
        OnAnimationFinished += (diceValue) =>
        {
            _normalDiceButton.interactable = true;
            _goldenDiceButton.interactable = true;
        };
    }

    private void LoadSprites()
    {
        _sprites = Resources.LoadAll<Sprite>("DiceImages").ToList();
    }

    // Coroutine (협동 루틴)
    // 어떤 함수가 반환 될 때 다른 함수를 호출해주고, 그 다른 함수도 반환 할 때 어떤 함수를 호출해주는
    // 상호 협동하는 관계의 함수관계를 협동 루틴이라고 한다

    // MonoBehavior 의 코루틴은 해당 코루틴을 호출한 MonoBehavior 의 Update 이벤트와 Coroutine 을 형성한다
    // 즉, 코루틴을 실행한 주체 MonoBehavior 가 비활성화 될 경우 (Update 이벤트 함수가 호출되지 못하게 될 경우),
    // 해당 Coroutine 은 돌아가지 않는다
    IEnumerator E_DiceAnimation(int diceValue)
    {
        OnAnimationStarted?.Invoke();

        float elapsedTime = 0.0f;
        while (elapsedTime < _animationTime)
        {
            if (_animationDelayTimer < 0)
            {
                _image.sprite = _sprites[Random.Range(0, _sprites.Count)];
                _animationDelayTimer = _animationDelay;
            }

            _animationDelayTimer -= Time.deltaTime;
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        _image.sprite = _sprites[diceValue - 1];

        // ? : Null조건 연산자
        OnAnimationFinished?.Invoke(diceValue);
    }
}
