using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text), typeof(Outline))]
public class MessageUI : MonoBehaviour
{
    [SerializeField] private float _alphaSpeed = 1.5f;

    private Text _text = null;
    private Outline _outline = null;

    private CancellationTokenSource _normal = null;

    private void OnValidate()
    {
        _text = GetComponent<Text>();
        _outline = GetComponent<Outline>();
    }

    private void Start()
    {
        UIManager.Instance.MessageUI.NormalEvent += Normal;

        _normal = new CancellationTokenSource();
    }

    private void OnDisable()
    {
        UIManager.Instance.MessageUI.NormalEvent -= Normal;
        _normal?.Cancel();
    }

    void Normal(string message)
    {
        _text.text = message;

        Color color = _text.color;
        color.a = 1;
        _text.color = color;

        color = _outline.effectColor;
        color.a = 1;
        _outline.effectColor = color;

        ReduceAlpha().Forget();
    }
    //徐々にAlphaを減らす
    async UniTask ReduceAlpha()
    {
        while(_text.color.a != 0 || _outline.effectColor.a != 0)
        {
            await UniTask.Yield(cancellationToken: _normal.Token);
            _text.color = ChangeAlpha(_text.color);
            _outline.effectColor = ChangeAlpha(_outline.effectColor);
        }

    }

    //徐々にAlphaを減らす
    private Color ChangeAlpha(Color color)
    {
        color.a = Mathf.MoveTowards(color.a, 0, _alphaSpeed * Time.deltaTime);
        return color;
    }

}
