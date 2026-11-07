using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class PointMagnificationUI : MonoBehaviour
{
    private Text _text = null;

    private void OnValidate()
    {
        _text = GetComponent<Text>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _text.text = 100.ToString();
        UIManager.Instance.MagnificationUI.PointMagnificationEvent += ChangeValue;
    }

    private void OnDisable()
    {
        UIManager.Instance.MagnificationUI.PointMagnificationEvent -= ChangeValue;
    }

    void ChangeValue(int mag)
    {
        _text.text = mag.ToString();
    }
}
