using UnityEngine;

public class StageEvent : MonoBehaviour
{
    [SerializeField] private StageController _stageController = null;

    private void OnBecameInvisible()
    {
        _stageController.ActiveChanger(false);
    }
}
