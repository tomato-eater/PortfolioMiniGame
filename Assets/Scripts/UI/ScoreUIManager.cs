using System;

/// <summary>
/// Scoreを表す指示用のクラス
/// </summary>
public class ScoreUIManager
{
    /// <summary>
    /// スコア表示を変更するためのイベント
    /// </summary>
    public event Action<int> ScoreEvent = null;

    /// <summary>
    /// Score更新(UI)
    /// </summary>
    /// <param name="score">更新後の数値</param>
    public void ChangeScore(int score)
    {
        ScoreEvent?.Invoke(score);
    }
}
