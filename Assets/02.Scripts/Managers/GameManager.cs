using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager>
{
    [Header("Camera")]
    public CameraController cameraController;

    [Header("Data")]
    private string savePath;
    public PlayerData playerData;
    public PlayerStat playerStat;
    public WeaponData curWeaponData;

    [Header("ObjectPool")]
    public DamageIndicatorPool damageIndicatorPool;


    public event Action OnAttackEvent;
    public event Action OnCriticalEvent;
    public event Action<int> OnAttackDamageEvent;

    protected override void Awake()
    {
        base.Awake();
        savePath = Path.Combine(Application.persistentDataPath, "playerData.json");

    }


    public void LoadGame()
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

    public void NewGame()
    {
        playerData = new PlayerData();
        StageManager.Instance.GenerateChapter(1);
        GameStart();
    }

    private void GameStart()
    {
        UIManager.Instance.FadeOut(() =>
        {
            SceneManager.sceneLoaded += OnSceneLoaded;//씬 로딩이 끝나면 실행되는 이벤트에 구독
            SceneManager.LoadScene("MainScene");
        });
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)//씬로디드에 구독하기 위해선 로드씬모드 매개변수 필수 매개변수 안넣을시 single로 고정 single외 Additive이 있음
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;//구독 취소해줘야함
        UIManager.Instance.FadeIn();
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
            bool isCrit = IsCrit();
            int damage = CalculateDamage(isCrit);
            targetEnemy.TakeDamage(damage);//나중에 크리티컬 여부 받아와야함
        }
        else return;
        
    }

    public void DamageEffect(int damage, bool IsCrit)
    {

    }

    Enemy GetRandomEnemy()
    {
        if(EnemyManager.Instance.enemies != null)
        {
            return EnemyManager.Instance.enemies[UnityEngine.Random.Range(0, EnemyManager.Instance.enemies.Length)];
        }
        return null;
    }

    int CalculateDamage(bool isCrit)
    {
        float baseDamage = curWeaponData.Weapon.baseAttack + curWeaponData.WeaponLevel*curWeaponData.Weapon.attackValum_Up;
        float critMultiplier = playerStat.critDamage.impressionStat * playerData.critDamageLevel;
        int totalDamage;

        if (isCrit)
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

