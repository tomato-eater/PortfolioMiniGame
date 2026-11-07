using UnityEngine;

/// <summary>
/// 背景を管理するクラス
/// </summary>
public class BackGroundController : MonoBehaviour
{
    [SerializeField] private Material _firstBack = null;
    [SerializeField] private Material _secondBack = null;
    [SerializeField] private Material _mostBack = null;

    private float _firstOffsetX = 0f;
    private float _secondOffsetX = 0f;
    private float _mostOffsetX = 0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (!_firstBack) Debug.LogError($"{gameObject.name} に _firstBack が未割当");
        if (!_secondBack) Debug.LogError($"{gameObject.name} に _secondBack が未割当");
        if (!_mostBack) Debug.LogError($"{gameObject.name} に _mostBack が未割当");

        _firstOffsetX = 0f;
        _secondOffsetX = 0f;
        _mostOffsetX = 0f;

        ChangeOffset();
    }

    /// <summary>
    /// Offset の値を変更・更新
    /// </summary>
    /// <param name="value">動かす基準値</param>
    public void ChangeValue(float value)
    {
        value *= 0.1f;

        _firstOffsetX += (value);
        _firstOffsetX %= 1;

        value *= 0.1f;

        _secondOffsetX += (value);
        _secondOffsetX %= 1;

        value *= 0.1f;

        _mostOffsetX += (value);
        _mostOffsetX %= 1;

        ChangeOffset();
    }

    /// <summary>
    /// Material の Offset を更新
    /// </summary>
    private void ChangeOffset()
    {
        _firstBack.mainTextureOffset = new Vector2(_firstOffsetX, 0);
        _secondBack.mainTextureOffset = new Vector2(_secondOffsetX, 0);
        _mostBack.mainTextureOffset = new Vector2(_mostOffsetX, 0);
    }
}
