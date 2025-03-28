using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    private string savePath;
    public PlayerData playerData;
    public PlayerStat playerStat;
    public WeaponData curWeaponData;

    public event Action OnAttackEvent;
    public event Action OnCriticalEvent;
    public event Action<int> OnAttackDamageEvent;

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
            StageManager.Instance.LoadStages(playerData);
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
        StageManager.Instance.GenerateChapter(1);
        GameStart();
    }

    private void GameStart()
    {
        SceneManager.LoadScene("MainScene");
        StageManager.Instance.SetupStage();
    }

    public void GameStop()
    {
        Time.timeScale = 0;
    }

    public void GameResume()
    {
        Time.timeScale = 1;
    }

    public void OnAttack()
    {
        Enemy targetEnemy = GetRandomEnemy();

        if (targetEnemy != null)
        {
            int damage = CalculateDamage();
        }
        else return;
        
        //OnAttackDamageEvent?.Invoke(damage);
    }

    Enemy GetRandomEnemy()
    {
        if(EnemyManager.Instance.Enemys != null)
        {
            return EnemyManager.Instance.Enemys[UnityEngine.Random.Range(0, EnemyManager.Instance.Enemys.Length)];
        }
        return null;
    }

    int CalculateDamage()
    {
        float baseDamage = curWeaponData.Weapon.baseAttack + curWeaponData.WeaponLevel*curWeaponData.Weapon.attackValum_Up;
        float critMultiplier = playerStat.critDamage.impressionStat * playerData.critDamageLevel;
        int totalDamage;

        if (IsCrit())
        {
            totalDamage = Mathf.RoundToInt(baseDamage * critMultiplier/100);
            OnCriticalEvent?.Invoke();
        }
        else
        {
            totalDamage = Mathf.RoundToInt(baseDamage);
        }

        return totalDamage;
    }

    bool IsCrit()
    {
        int critChance = Mathf.RoundToInt(curWeaponData.Weapon.baseCriticalChance);
        int randValue = UnityEngine.Random.Range(0, 100);

        return critChance >= randValue;
    }



    public void GetCoin(int value)
    {
        playerData.coin += value;
        UIManager.Instance.coinDisplayUI.SetCoinText();
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
        UIManager.Instance.coinDisplayUI.SetCoinText();
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

