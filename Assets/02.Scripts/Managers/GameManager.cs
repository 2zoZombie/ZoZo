using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    private string savePath;
    public PlayerData playerData;

    protected override void Awake()
    {
        base.Awake();
        savePath = Path.Combine(Application.persistentDataPath, "playerData.json");
    }


    void LoadGame()
    {
        if (File.Exists(savePath))
        {
            string json = File.ReadAllText(savePath);
            playerData = JsonUtility.FromJson<PlayerData>(json);
            GameStart();
        }
        else
        {
            Debug.Log("로드데이터 없음");
        }
    }

    public void SaveGame()
    {
        string json = JsonUtility.ToJson(playerData, true);
        File.WriteAllText(savePath, json);
    }

    void NewGame()
    {
        playerData = new PlayerData();
        GameStart();
    }

    private void GameStart()
    {

    }

    public void GetCoin(int value)
    {
        playerData.coin += value;
    }

    /// <summary>
    /// 코인 사용할일 있다면 if에 넣고 사용
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public bool SpendCoin(int value)
    {
        if (playerData.coin >= value)
        {
            playerData.coin -= value;
            return true;
        }

        UIManager.Instance.errorPopup.SetErrorText("coin is not Enough");
        return false;
    }

    public void GetBlueCoin(int value)
    {
        playerData.blueCoin += value;
    }

    /// <summary>
    /// 코인 사용할일 있다면 if에 넣고 사용
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public bool SpendBlueCoin(int value)
    {
        if (playerData.coin >= value)
        {
            playerData.coin -= value;
            return true;
        }

        UIManager.Instance.errorPopup.SetErrorText("bluecoin is not Enough");
        return false;
    }
}

