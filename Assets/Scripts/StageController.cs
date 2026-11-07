using UnityEngine;

public class StageController : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _spriteRenderer = null;

    private void Start()
    {
        if (!_spriteRenderer)
        {
            Debug.LogError($"{gameObject.name} に SpriteRenderer が未割当 ");
        }
    }

    /// <summary>
    /// 描画状態の更新
    /// </summary>
    /// <param name="active">描画 ? する : しない</param>
    public void ActiveChanger(bool active) => gameObject.SetActive(active);
    
    /// <summary>
    /// 座標の取得
    /// </summary>
    /// <returns>ポジション</returns>
    public Vector3 Position() => _spriteRenderer.transform.position;
}
