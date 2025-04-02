using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Tilemaps;

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


public class StageManager : Singleton<StageManager>
{
    [Header("StageInfo")]
    public ChapterNameSO stageNameSO;
    public int stageMinThreshold = 5;
    public int stageMaxThreshold = 8;

    public int currentChapter = 1;
    public int currentStage = 0;
    public List<StageData> currentStages;
    ChapterInfo currentChapterInfo;
    GameObject currentTilemap;

    public UnityEvent onStageComplete = new UnityEvent();
    public UnityEvent onStageStart = new UnityEvent();
    public UnityEvent onChapterStart = new UnityEvent();

    private void Start()
    {
        //GenerateChapter();
    }

    public void LoadStages(PlayerData data)
    {
        currentStages = data.stageDatas;
        currentChapter = data.currentChapter;
        currentStage = data.currentStage;
        currentChapterInfo = data.currentChapterInfo;
    }

    public void GenerateChapter()
    {
        currentStages = new List<StageData>();

        int stageCount = Random.Range(stageMinThreshold, stageMaxThreshold + 1);
        SelectChapter();
        SetChapter();
        string chapterName = GetChapterName();

        for (int i = 0; i < stageCount; i++)
        {
            StageType type;

            if (i == stageCount - 1)
                type = StageType.Treasure;
            else if (i == stageCount - 2)
                type = StageType.Boss;
            else
                type = StageType.Normal;

            currentStages.Add(new StageData(i + 1, type, GetStageName(type, currentChapter, chapterName, i + 1), false));
        }

        SetStageDataToPlayerData();
        currentStage = 0;
        onChapterStart?.Invoke();
        SetupStage();
    }

    void SelectChapter()
    {
        currentChapterInfo = stageNameSO.chapterInfo[Random.Range(0, stageNameSO.chapterInfo.Length)];
    }

    string GetChapterName()
    {
        return stageNameSO.chapterAdjective[Random.Range(0, stageNameSO.chapterAdjective.Length)] + " " + currentChapterInfo.chapterName;
    }

    string GetStageName(StageType type, int chapter, string chapterName, int stage)
    {
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

        return $"챕터 {chapter} {chapterName} \n{stageName}";
    }

    public void SetChapter()
    {
        SetTilemap();
        SetBGM();
    }

    void SetTilemap()
    {
        if (currentTilemap != null)
        {
            Destroy(currentTilemap.gameObject);
        }

        // 새 타일맵 생성
        if (currentChapterInfo.tilemap != null)
        {
            currentTilemap = Instantiate(currentChapterInfo.tilemap, Vector3.zero, Quaternion.identity);
        }
    }

    void SetBGM()
    {
        AudioManager.Instance.PlayBGM(currentChapterInfo.bgm);
    }

    public void SetupStage()
    {
        StageData stage = currentStages[currentStage];
        Debug.Log($"{stage.stageName} 시작");

        // 타입별 로직 실행 각 몬스터 소환 하면 됨
        EnemyManager.Instance.SpawnEnemy(stage.stageType);
        onStageStart?.Invoke();
    }

    public void CompleteStage()
    {
        currentStages[currentStage].isCleared = true;
        currentStage++;
        SetCurrentInfoToPlayerData();
        onStageComplete?.Invoke();

        if (currentStage >= currentStages.Count)
        {
            currentChapter++;
            Invoke("GenerateChapter", 4f);
        }
        else
        {
            Invoke("SetupStage", 4f);
        }
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
