using UnityEngine;

/// <summary>
/// スコアを管理するクラス
/// </summary>
public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; } = null;

    public int Score { get; private set; } = 0;
    private float _scoreMag = 1;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        Instance = this;
    }

    private void OnDestroy()
    {
        Instance = null;
    }

    /// <summary>
    /// ポイントをスコアに足す
    /// </summary>
    /// <param name="addScore">足したい値</param>
    public void AddScore(int addScore)
    {
        Score += (int)(addScore * _scoreMag);
        UIManager.Instance.ScoreUI.ChangeScore(Score);
    }

    /// <summary>
    /// 倍率を上げる
    /// </summary>
    /// <param name="mag">足したい値</param>
    public void AddMag(float  mag)
    {
        _scoreMag += mag;
        UIManager.Instance.MagnificationUI.ChangeMagnification(_scoreMag);
    }
}
