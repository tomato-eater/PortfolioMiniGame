using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ステージを保管するクラス
/// </summary>
[CreateAssetMenu(fileName = "StageList", menuName = "Scriptable Objects/StageList")]
public class StageList : ScriptableObject
{
    [SerializeField] private List<GameObject> _stage = new();

    /// <summary>
    /// ステージのプレハブを取得
    /// </summary>
    public List<GameObject> Stage => _stage;
}
