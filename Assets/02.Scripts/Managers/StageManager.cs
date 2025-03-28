using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : Singleton<StageManager>
{
    PlayerData playerData;

    [Header("StageInfo")]
    public int stageMinThreshold = 5;
    public int stageMaxThreshold = 8;
   


    public void SetPlayerData()
    {
        playerData = GameManager.Instance.playerData;
    }

    void NextStage()
    {

    }

    void NextChapter()
    {
        playerData.currentChapter++;
    }
}
