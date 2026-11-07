using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class ScoreUI : MonoBehaviour
{
    private Text _text = null;

    private void OnValidate()
    {
        _text = GetComponent<Text>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _text.text = 0.ToString();
        UIManager.Instance.ScoreUI.ScoreEvent += ChangeScore;
    }

    private void OnDisable()
    {
        UIManager.Instance.ScoreUI.ScoreEvent -= ChangeScore;
    }

    void ChangeScore(int score)
    {
        _text.text = score.ToString();
    }
}
