using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CoinTilemap : MonoBehaviour
{
    [SerializeField, HideInInspector] Tilemap _tilemap;

    [SerializeField, Tooltip("追加する倍率(小数点以下)")] float _point;

    [SerializeField, Tooltip("追加するスコア(整数)")] int _score;

    Dictionary<Vector3Int, TileBase> _collectedCoins = new(); 

    private void OnValidate()
    {
        _tilemap = GetComponent<Tilemap>();
    }


    private void OnEnable()
    {
        if (_tilemap == null || _collectedCoins.Count == 0) return;

        foreach (var pair in _collectedCoins)
        {
            // 記録していた座標に元のタイルを再配置
            _tilemap.SetTile(pair.Key, pair.Value);
        }

        // 記録をクリア
        _collectedCoins.Clear();
    }


    void OnTriggerEnter2D(Collider2D other) => CollectCoinsInBounds(other);
    void OnTriggerStay2D(Collider2D other) => CollectCoinsInBounds(other);

    private void CollectCoinsInBounds(Collider2D other)
    {
        // プレイヤーのコライダーの「左下(min)」と「右上(max)」のワールド座標をセル座標に変換
        // 境界線ギリギリの誤爆を防ぐため、ごくわずかに(0.05f)内側に縮める
        Bounds bounds = other.bounds;
        Vector3 minWorld = bounds.min + new Vector3(0.05f, 0.05f, 0);
        Vector3 maxWorld = bounds.max - new Vector3(0.05f, 0.05f, 0);

        Vector3Int minCell = _tilemap.WorldToCell(minWorld);
        Vector3Int maxCell = _tilemap.WorldToCell(maxWorld);

        // プレイヤーの体が重なっているすべてのセルをループでチェック
        for (int x = minCell.x; x <= maxCell.x; x++)
        {
            for (int y = minCell.y; y <= maxCell.y; y++)
            {
                Vector3Int cellPos = new Vector3Int(x, y, 0);
                TileBase tile = _tilemap.GetTile(cellPos);

                if (tile != null)
                {
                    _collectedCoins[cellPos] = tile;
                    _tilemap.SetTile(cellPos, null);

                    ScoreManager.Instance.AddMag(_point);
                    ScoreManager.Instance.AddScore(_score);
                }
            }
        }
    }
}