using UnityEngine;

/// <summary>
/// UIを司るクラス
/// </summary>
public class UIManager : MonoBehaviour
{
    /// <summary>
    /// UIの
    /// </summary>
    public static UIManager Instance { get; private set; } = null;  

    /// <summary>
    /// メッセージUIの
    /// </summary>
    public MessageUIManager MessageUI { get; private set; } = new();

    /// <summary>
    /// ポイント倍率UIの
    /// </summary>
    public PointMagnificationUIManager MagnificationUI { get; private set; } = new();

    /// <summary>
    /// スコアのUIの
    /// </summary>
    public ScoreUIManager ScoreUI { get; private set; } = new();


    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
        }
        Instance = this;
    }

    private void OnDestroy()
    {
        Instance = null;
    }
}
