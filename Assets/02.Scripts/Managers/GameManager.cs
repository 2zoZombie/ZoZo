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
    public Player player;
    public ClickHandler clickHandler;
    public WeaponData curWeaponData;

    [Header("ObjectPool")]
    public DamageIndicatorPool damageIndicatorPool;
    public DropItemPool dropItemPool;
    public DropItemCollector dropItemCollector;


    bool isLoaded = false;

    //public event Action OnAttackEvent;
    public event Action OnCriticalEvent;
    //public event Action<int> OnAttackDamageEvent;
    public event Action OnCoinChange;
    public event Action OnBlueCoinChange;

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
            isLoaded = true;
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
        isLoaded = false;
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
        if (isLoaded)
        {
            StageManager.Instance.SetChapter();
            StageManager.Instance.SetupStage();
        }
        else
        {
            StageManager.Instance.GenerateChapter();
        }
    }

    public void GameStop()
    {
        Time.timeScale = 0;
    }

    public void GameResume()
    {
        Time.timeScale = 1;
    }

    public void OnAttack(GameObject target = null)
    {
        IAttackable targetEnemy;
        if (target == null) targetEnemy = GetRandomEnemy();
        else targetEnemy = target.GetComponent<IAttackable>();

        if (targetEnemy != null)
        {
            player.PlayerState = StateType.Attack;
            bool isCrit = IsCrit();
            int damage = CalculateDamage(isCrit);
            targetEnemy.TakeDamage(damage, isCrit);//나중에 크리티컬 여부 받아와야함
        }
        else return;

    }

    public void DamageEffect(int damage, bool IsCrit, Transform position)
    {
        DamageIndicator dmg = damageIndicatorPool.GetFromPool(position).GetComponent<DamageIndicator>();
        dmg.Show(damage, IsCrit);
        if (IsCrit) cameraController.Shake(3f, 3f, 0.45f);
        else cameraController.Shake(1f, 1f, 0.3f);
    }

    IAttackable GetRandomEnemy()
    {
        if (EnemyManager.Instance.enemies != null)
        {
            return EnemyManager.Instance.enemies[UnityEngine.Random.Range(0, EnemyManager.Instance.enemies.Count)];
        }
        return null;
    }

    int CalculateDamage(bool isCrit)
    {
        float baseDamage = curWeaponData.weaponSO.baseAttack + curWeaponData.weaponLevel * curWeaponData.weaponSO.attackVolume_Up;
        baseDamage *= UnityEngine.Random.Range(0.9f, 1.1f);
        float critMultiplier = player.critDamage.impressionStat * playerData.critDamageLevel;
        int totalDamage;

        if (isCrit)
        {
            totalDamage = Mathf.RoundToInt(baseDamage * (1 + critMultiplier / 100));
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
        int critChance = Mathf.RoundToInt(curWeaponData.weaponSO.baseCriticalChance);
        int randValue = UnityEngine.Random.Range(0, 100);

        return critChance >= randValue;
    }



    public void GetCoin(int value)
    {
        playerData.coin += value;
        OnCoinChange?.Invoke();
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
            OnCoinChange?.Invoke();
            return true;
        }

        UIManager.Instance.errorPopup.ShowErrorMessage("coin is not Enough");
        return false;
    }

    public void GetBlueCoin(int value)
    {
        playerData.blueCoin += value;
        OnBlueCoinChange?.Invoke();
    }

    /// <summary>
    /// 코인 사용할일 있다면 if에 넣고 사용
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public bool SpendBlueCoin(int value)
    {
        if (playerData.blueCoin >= value)
        {
            playerData.blueCoin -= value;
            return true;
        }

        OnBlueCoinChange?.Invoke();
        UIManager.Instance.errorPopup.ShowErrorMessage("bluecoin is not Enough");
        return false;
    }
}

