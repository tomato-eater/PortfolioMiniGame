using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    [SerializeField, Min(0)] private float _initialMoveSpeed = 5;

    [SerializeField, Min(5)] private int _stageMaxinumNumber = 5;

    [SerializeField] private BackGroundController _backGroundController = null;

    [SerializeField] private StageController _startStage = null;

    [SerializeField] private StageList _stageList = null;

    private List<StageController> _standByStage = new();

    private List<StageController> _usingStage = new();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(!_backGroundController) Debug.LogError($"{gameObject.name} に BackGroundController が未割当");
        
        //ステージの生成・登録
        foreach ( GameObject stage in _stageList.Stage)
        {
            GameObject stageObject = Instantiate(stage);
            stageObject.transform.position = new Vector3(0, -5, 0);
            stageObject.SetActive(false);
            _standByStage.Add(stageObject.GetComponent<StageController>());
        }

        int stageCount = Mathf.Min(_standByStage.Count - 1, _stageMaxinumNumber);
        for (int i = 0; i < stageCount; i++)
        {
            StageSet(0);
        }
    }

    /// <summary>
    /// 地面や背景を動かす
    /// </summary>
    /// <param name="speed">速度の倍率</param>
    public void UpdateStage(float speed)
    {
        float moveSpeed = (_initialMoveSpeed * speed) * Time.deltaTime;

        _backGroundController.ChangeValue(moveSpeed);

        if(_startStage.gameObject.activeSelf)
        {
            _startStage.transform.Translate(Vector3.left * moveSpeed);
        }

        int usingStageNum = 0;
        for(int i =0; i< _usingStage.Count; i++)
        {
            if (!_usingStage[usingStageNum].gameObject.activeSelf)
            {
                StageSet(Random.Range(0, _standByStage.Count));
                //ステージを待機listに移動
                _standByStage.Add(_usingStage[usingStageNum]);
                _usingStage.Remove(_usingStage[usingStageNum]);
                continue;
            }

            _usingStage[usingStageNum].transform.Translate(Vector3.left * moveSpeed);
            usingStageNum++;
        }

    }

    /// <summary>
    /// ステージの設置
    /// </summary>
    private void StageSet(int num)
    {
        _standByStage[num].transform.position = _usingStage.Count > 0 ? _usingStage[_usingStage.Count - 1].Position() : _startStage.Position();
        _standByStage[num].ActiveChanger(true);
        _usingStage.Add(_standByStage[num]);
        _standByStage.Remove(_standByStage[num]);
    }

}
