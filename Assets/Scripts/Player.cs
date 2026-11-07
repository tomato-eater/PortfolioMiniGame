using Cysharp.Threading.Tasks;
using System;
using System.Threading;
using UnityEngine;


[RequireComponent(typeof(Animator), typeof(SpriteRenderer), typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
    [SerializeField] private InputManager _inputManager = null;
    [SerializeField] private LayerMask _groundMask = 10;
    [SerializeField] private float _defaultPositionX = -1.8f;
    [SerializeField, Range(0.1f, 5.25f)] private float _jumpPower = 5;
    [SerializeField] private float _returnSpeed = 50.0f;

    [SerializeField] private AnimatorOverrideController _warmingAnimator = null;
    [SerializeField] private AnimatorOverrideController _runAnimator = null;

    private Animator _animator = null;
    private SpriteRenderer _spriteRenderer = null;
    private Rigidbody2D _rigidbody2D = null;

    private bool _canAirJump = true;

    private bool _canOperation = false;
    private bool _isPlay = false;

    /// <summary>
    /// 死んだことを伝えるイベント
    /// </summary>
    internal event Action DeathEvent = null; 

    private void OnValidate()
    {
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        _canOperation = false;
        _isPlay = false;

        _animator.runtimeAnimatorController = _warmingAnimator;
    }

   void OnBecameInvisible()
    {
        DeathEvent?.Invoke();
        gameObject.SetActive(false);
    }

    /// private void WarmingUp() => _animator.Play("Emotion.Warmingup");
    /// <summary>
    /// 開始前に再生する
    /// </summary>
    public async UniTask PlayWarningUp(CancellationToken token)
    {
        _animator.Play("Emotion.Warmingup");

        await UniTask.Yield(cancellationToken: token);

        await UniTask.WaitUntil(() =>
        {
            AnimatorStateInfo info = _animator.GetCurrentAnimatorStateInfo(0);
            return info.IsName("Emotion.Warmingup") && info.normalizedTime >= 1.0f;
        }, cancellationToken: token);
    }

    /// <summary>
    /// Playerの操作を可能にする(ジャンプができるようになる)
    /// </summary>
    public void CanOperation()
    {
        _animator.runtimeAnimatorController = _runAnimator;
        _canOperation = true;
    }

    /// <summary>
    /// ゲーム開始
    /// </summary>
    public void GameStart() => _isPlay = true;

    // Update is called once per frame
    void Update()
    {
        //入力が有効かの確認
        if(!_inputManager)
        {
            Debug.LogError($"{gameObject.name}に InputManager が未割当");
            return;
        }

        if (!_canOperation) return;

        //現在地の取得
        Vector3 currentPosition = transform.position;

        //床との距離を取得
        float groundDistance = GroundDistance(currentPosition);
        bool canJump = groundDistance <= 0;
        if( canJump )
        {
            _canAirJump = true;
        }

        //今の速度を取得・変更
        Vector2 velocity = _rigidbody2D.linearVelocity;

        //高さ
        if (_inputManager.JumpTrigger && JumpJudge(canJump))
        {
            velocity.y = _jumpPower;
        }
        else if (_inputManager.JumpRelease && velocity.y >= 0)
        {
            velocity.y *= 0.5f;
        }

        //横
        if (Mathf.Abs(currentPosition.x - _defaultPositionX) >= 0.01f && groundDistance <= 0.1f) 
        {
            velocity.x = (currentPosition.x < _defaultPositionX ? _returnSpeed : -_returnSpeed) * Time.deltaTime;
        }
        else
        {
            velocity.x = 0;
        }

        //速度を更新
        _rigidbody2D.linearVelocity = velocity;

        //アニメーションの再生
        _animator.SetFloat("GroundDistance", groundDistance);
        _animator.SetFloat("FallSpeed", _rigidbody2D.linearVelocity.y);
        _animator.SetFloat("Speed", _isPlay ? 1.0f : 0.0f);
    }

    /// <summary>
    /// 地面との距離
    /// </summary>
    /// <param name="currentPosition">現在地</param>
    /// <returns>地面との距離</returns>
    private float GroundDistance(Vector2 currentPosition)
    {
        RaycastHit2D hit = Physics2D.Raycast(currentPosition, Vector2.down, 1.0f, _groundMask);
        return hit ? hit.distance : float.MaxValue; 
    }

    /// <summary>
    /// ジャンプの判定
    /// </summary>
    /// <param name="canJump">地面に設置しているかの判断</param>
    /// <returns>ジャンプ出来ま ? す :　せん</returns>
    private bool JumpJudge(bool canJump)
    {
        //接地している
        if(canJump)
        {
            return true;
        }
        //空中だが飛べる
        else if(_canAirJump)
        {
            _canAirJump = false;
            return true;
        }
        //飛べない
        return false;
    }

}
