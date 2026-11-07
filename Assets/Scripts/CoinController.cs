using UnityEngine;

public class CoinController : MonoBehaviour
{
    private void OnEnable()
    {
        int childCount = transform.childCount;
        for(int i = 0; i < childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(true);
        }
    }
}
