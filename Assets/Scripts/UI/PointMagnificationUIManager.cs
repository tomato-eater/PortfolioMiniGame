using System;
using System.Diagnostics;
using UnityEngine;

/// <summary>
/// Pointの取得倍率を表す指示用のクラス
/// </summary>
public class PointMagnificationUIManager
{
    /// <summary>
    /// 変更を伝えるためのイベント
    /// </summary>
    public event Action<int> PointMagnificationEvent = null;

    /// <summary>
    /// ポイント倍率変更(UI)
    /// </summary>
    /// <param name="magnification">floatのまま渡す</param>
    public void ChangeMagnification(float magnification)
    {
        PointMagnificationEvent?.Invoke(Mathf.RoundToInt(magnification * 100.0f));
    }
}