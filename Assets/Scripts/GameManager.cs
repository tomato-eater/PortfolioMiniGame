using Cysharp.Threading.Tasks;
using System;
using System.Threading;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; } = null;

    [SerializeField] private Player _player = null;
    [SerializeField] private float _firstWait = 0.5f;
    [SerializeField] private int _countDown = 1;
    [SerializeField, Min(1)] private float _maxMag = 3;
    [SerializeField] private float _addMagTimme = 1;
    [SerializeField] private float _addScoreTime = 0.2f;

    [SerializeField] private StageManager _stageManager;

    private bool _isPlay = false;

    private float _mag = 1;

    private bool _death = false;


    //キャンセルトークン
    private CancellationTokenSource _myToken = null; 
    private CancellationTokenSource _otherToken = null;
    private CancellationTokenSource _magToken = null;
    private CancellationTokenSource _scoreToken = null;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        Instance = this;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(!_player)
        {
            Debug.Log($"{gameObject.name} に Player が未割当");
        }

        _isPlay = false;

        _player.DeathEvent += DeathPlayer;

        _myToken = new ();
        _otherToken = new ();
        _magToken = new ();
        _scoreToken = new ();
        BeforeStarting().Forget();
    }

    private void OnDisable()
    {
        _otherToken?.Cancel();
        _otherToken?.Dispose();
        _otherToken = null;

        _myToken?.Cancel();
        _myToken?.Dispose();
        _myToken = null;

        _magToken?.Cancel();
        _magToken?.Dispose();
        _magToken = null;

        _scoreToken?.Cancel();
        _scoreToken?.Dispose();
        _scoreToken = null; 

        _player.DeathEvent -= DeathPlayer;
    }

    private void OnDestroy()
    {
        Instance = null;
    }

    /// <summary>
    /// ゲームの開始前の処理
    /// </summary>
    /// <returns>Void</returns>
    async UniTaskVoid BeforeStarting()
    {
        await UniTask.Delay(TimeSpan.FromSeconds(_firstWait), cancellationToken: _myToken.Token);

        //Playerの ウォーミングアップ を再生後、ジャンプ可能にする
        await _player.PlayWarningUp(_otherToken.Token);
        _player.CanOperation();

        UIManager.Instance.MessageUI.NormalMessage("Rady...");

        await UniTask.Delay(TimeSpan.FromSeconds(_countDown), cancellationToken: _myToken.Token);

        UIManager.Instance.MessageUI.NormalMessage("GO!!");

        _player.GameStart();
        _isPlay = true;
        AddMag().Forget();
        AddTimeScore().Forget();
    }

    private void Update()
    {
        if (!_isPlay) return;

        if (!_death)
        {
            _stageManager.UpdateStage(_mag);
        }
        else
        {

        }
    }

    /// <summary>
    /// 倍率を1%ずつ上げる
    /// </summary>
    /// <returns></returns>
    async UniTaskVoid AddMag()
    {
        while (_mag < _maxMag)

        {
            await UniTask.Delay(TimeSpan.FromSeconds(_addMagTimme), cancellationToken: _magToken.Token);

            _mag += 0.01f;

            _mag = (float)Math.Round(_mag, 3);
        }

        _magToken?.Dispose();
        _magToken = null;
    }
    
    /// <summary>
    /// Marks the player as dead.
    /// </summary>
    private void DeathPlayer()
    {
        _death = true;
        
        _scoreToken?.Cancel();
        _scoreToken?.Dispose();
        _scoreToken = null; 
    }

    async UniTask AddTimeScore()
    {
        while (!_death)
        {
            await UniTask.Delay(TimeSpan.FromSeconds(_addScoreTime), cancellationToken: _scoreToken.Token);
            ScoreManager.Instance.AddScore(1);
        }
    }

}
