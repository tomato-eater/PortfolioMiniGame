using System;

/// <summary>
/// メッセージの指示用のクラス
/// </summary>
public class MessageUIManager
{
    /// <summary>
    /// 普通のメッセージを出すためのイベント
    /// </summary>
    public event Action<string> NormalEvent = null;

    public void NormalMessage(string message)
    {
        NormalEvent?.Invoke(message);
    }


}