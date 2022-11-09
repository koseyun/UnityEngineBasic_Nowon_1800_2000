using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Video;

public class NoteSpawnManager : MonoBehaviour
{
    public static NoteSpawnManager Instance;

    [SerializeField] private Transform _spawnersParent;
    [SerializeField] private Transform _hittersParent;
    [SerializeField] private VideoPlayer _videoPlayer;

    public float NoteSpeedScale = 4.0f;
    public float NoteFallingDistance => _spawnersParent.position.y - _hittersParent.position.y;
    public float NoteFallingTime => NoteFallingDistance / NoteSpeedScale;

    private Dictionary<KeyCode, NoteSpawner> _spawners = new Dictionary<KeyCode, NoteSpawner>();
    private Queue<NoteData> _noteDataQueue = new Queue<NoteData>();

    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        StartCoroutine(E_Init());
    }

    public void StartSpawn()
    {
        if (_noteDataQueue.Count > 0)
        {
            StartCoroutine(E_Spawning());
            Invoke("PlayVideo", NoteFallingTime);
        }
    }

    IEnumerator E_Spawning()
    {
        float timeMark = Time.time; //"Time.time 현재시간
        NoteData noteData;

        while (_noteDataQueue.Count > 0)
        {
            while (_noteDataQueue.Count > 0)
            {
                // 가장 앞에 있는 큐의 시간이 경과한 시간보다 작으면 해당 데이터 노트 소환
                if (_noteDataQueue.Peek().Time < Time.time - timeMark)
                //.의 제일 위에 있는 친구.의 시간
                {
                    noteData = _noteDataQueue.Dequeue(); //"Dequeue() 맨앞 데이터 뽑아버림
                    _spawners[noteData.Key].Spawn();
                }
                else
                {
                    break;
                }
            }
            
            yield return null;
        }
    }
    
    IEnumerator E_Init()
    {
        foreach (NoteSpawner noteSpawner in _spawnersParent.GetComponentsInChildren<NoteSpawner>())
        {
            _spawners.Add(noteSpawner.Key, noteSpawner);
        }

        // WaitUntil 객체 : Func<bool> 의 반환값이 true 가 될 때까지 기다렸다가 다음 yield 실행하는 객체
        yield return new WaitUntil(() => SongSelector.Instance != null &&
                                         SongSelector.Instance.IsLoaded);

        IOrderedEnumerable<NoteData> noteDataFiltered = SongSelector.Instance.Data.Notes.OrderBy(note => note.Time);
        foreach (NoteData noteData in noteDataFiltered)
            _noteDataQueue.Enqueue(noteData);

        StartSpawn();
    }

    private void PlayVideo()
    {
        _videoPlayer.clip = SongSelector.Instance.Clip;
        _videoPlayer.Play();
    }
}
