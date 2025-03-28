using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StageType
{
    Normal,
    Boss,
    Treasure
}

[System.Serializable]
public class StageData
{
    public int stageNumber;
    public StageType stageType;
    public string stageName;
    public bool isCleared;

    public StageData(int stageNumber, StageType stageType, string stageName, bool isCleared)
    {
        this.stageNumber = stageNumber;
        this.stageType = stageType;
        this.stageName = stageName;
        this.isCleared = isCleared;
    }
}

[CreateAssetMenu(fileName = "Chapter", menuName = "ChapterNameList")]
public class ChapterNameSO : ScriptableObject
{
    public string[] chapterAdjective;
    public string[] chapterName;
}

public class StageManager : Singleton<StageManager>
{
    [Header("StageInfo")]
    public ChapterNameSO stageNameSO;
    public int stageMinThreshold = 5;
    public int stageMaxThreshold = 8;

    public int currentChapter = 1;
    public int currentStage = 0;
    public List<StageData> currentStages;

    private void Start()
    {
    }

    public void LoadStages(PlayerData data)
    {
        currentStages = data.stageDatas;
        currentChapter = data.currentChapter;
        currentStage = data.currentStage;
    }

    public void GenerateChapter(int chapterNumber)
    {
        currentStages = new List<StageData>();

        int stageCount = Random.Range(stageMinThreshold, stageMaxThreshold + 1);

        for (int i = 0; i < stageCount; i++)
        {
            StageType type;

            if (i == stageCount - 1)
                type = StageType.Treasure;
            else if (i == stageCount - 2)
                type = StageType.Boss;
            else
                type = StageType.Normal;

            currentStages.Add(new StageData(i + 1, type, GetStageName(type, chapterNumber, i + 1), false));
        }

        SetStageDataToPlayerData();
        currentStage = 0;
    }

    string GetStageName(StageType type, int chapter, int stage)
    {
        string chapterName = stageNameSO.chapterAdjective[Random.Range(0, stageNameSO.chapterAdjective.Length)] + stageNameSO.chapterName[Random.Range(0, stageNameSO.chapterName.Length)];
        string stageName;

        switch (type)
        {
            case StageType.Normal:
                stageName = $"스테이지 {stage}";
                break;
            case StageType.Boss:
                stageName = "보스 스테이지";
                break;
            case StageType.Treasure:
                stageName = "보물방";
                break;
            default:
                stageName = "";
                break;
        }

        return $"챕터 {chapter} {chapterName} {stageName}";
    }

    public void SetupStage()
    {
        StageData stage = currentStages[currentStage];
        Debug.Log($"{stage.stageName} 시작");

        // 타입별 로직 실행 각 몬스터 소환 하면 됨
        switch (stage.stageType)
        {
            case StageType.Normal:
                break;
            case StageType.Boss:
                break;
            case StageType.Treasure:
                break;
            default:
                break;
        }

    }

    public void CompleteStage()
    {
        currentStages[currentStage].isCleared = true;
        currentStage++;

        if (currentStage >= currentStages.Count)
        {
            currentChapter++;
            GenerateChapter(currentChapter);
        }

        SetCurrentInfoToPlayerData();
        SetupStage();
    }

    void SetCurrentInfoToPlayerData()
    {
        PlayerData playerData = GameManager.Instance.playerData;
        playerData.currentChapter = currentChapter;
        playerData.currentStage = currentStage;
    }

    void SetStageDataToPlayerData()
    {
        PlayerData playerData = GameManager.Instance.playerData;

        if (playerData.stageDatas != null) playerData.stageDatas.Clear();

        playerData.stageDatas = currentStages;
    }
}
