using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Skill : MonoBehaviour
{
    public SkillSO data;
    public Image skillImage;

    public Button levelupBtn;
    public TextMeshProUGUI btnText;
    public TextMeshProUGUI _name;
    public TextMeshProUGUI description;

    public EventTrigger trigger;

    private int indexNum;
    private int currentPrice;

    private int currentLevel;
    public int CurrentLevel
    {
        get
        {
            return currentLevel;
        }
        set
        {
            //이미 한계 레벨이라면 변경하지 않는다.
            if (!CheckMaxLevel(CurrentLevel)) return;

            //플레이어가 현재 가격만큼의 코인을 가지고 있는지 체크 (false시 실행X)
            if (!GameManager.Instance.SpendCoin(currentPrice))
            {
                return;
            }

            //CurrentLevel이 바뀔 때마다 playerData의 값을 변경
            currentLevel = value;
            GameManager.Instance.playerData.statLevel[indexNum] = currentLevel;

            //가격 갱신
            currentPrice *= data.impressionPrice;

            //UI 갱신
            UIRefresh(data.index);
        }
    }

    private float timer = 0.0f;
    private bool startTimer = false;
    private bool isHolding = false;

    private void OnValidate()
    {
        skillImage = transform.Find("Icon")?.GetComponent<Image>();
        levelupBtn = transform.Find("LevelUpBtn")?.GetComponent<Button>();
        btnText = levelupBtn.GetComponentInChildren<TextMeshProUGUI>();

        _name = transform.Find("Name")?.GetComponent<TextMeshProUGUI>();
        description = transform.Find("Description")?.GetComponent<TextMeshProUGUI>();

        skillImage.sprite = data.Icon;

        //SkillOB가 할당되지 않았다면 오류 문구 출력하고 이하 과정 생략
        if (data == null)
        {
            Debug.Log($"{this.name}에 SkillOB가 할당되지 않았습니다.");
            return;
        }

        //레벨업 버튼에 메서드 할당
        levelupBtn.onClick.AddListener(SkillLevelUp);
        //현재 가격 초기화
        currentPrice = data.basicPrice;
        indexNum = (int)data.index;

        //이벤트 할당
        trigger = transform.Find("LevelUpBtn")?.GetComponent<EventTrigger>();
        if (trigger == null)
        {
            transform.Find("LevelUpBtn")?.AddComponent<EventTrigger>();
        }

        //이벤트 붙여주기
        EventTrigger.Entry entryDown = new EventTrigger.Entry();
        EventTrigger.Entry entryUp = new EventTrigger.Entry();

        entryDown.eventID = EventTriggerType.PointerDown;
        entryUp.eventID = EventTriggerType.PointerUp;

        entryDown.callback.AddListener((data) => { OnPointerDown(); });
        entryUp.callback.AddListener((data) => { OnPointerUp(); });

        //OnValidate라 자꾸 여러개가 붙어서 추가함...
        trigger.triggers.Clear();

        trigger.triggers.Add(entryDown);
        trigger.triggers.Add(entryUp);
    }


    private void Start()
    {
        //현재 레벨을 로드한 플레이어 데이터대로 초기화
        currentLevel = GameManager.Instance.playerData.statLevel[indexNum];

        //소지 코인에 변화가 있을 때 실행되는 델리게이트에 CheckEnoughCoins를 구독시킴
        GameManager.Instance.OnCoinChange += CheckEnoughCoins;

        //UI 갱신
        UIRefresh(data.index);
    }

    private void Update()
    {
        //오래 누르면 연속강화
        if (startTimer)
        {
            timer += Time.deltaTime;

            if (timer >= 1f)
            {
                isHolding = true;
            }

        }

        if (isHolding)
        {
            if (timer >= 0.2f)
            {
                SkillLevelUp();
                timer = 0.0f;
            }
        }
    }

    public void SkillLevelUp()
    {
        //해당 스텟 레벨을 1 증가시킴
        CurrentLevel++;
        switch(data.index)
        {
            case StatIndex.CriticalDamage:
                GameManager.Instance.playerData.critDamageLevel++;
                break;
            case StatIndex.AutoAttackInterval:
                GameManager.Instance.playerData.autoAttackLevel++;
                break;
            case StatIndex.GoldGainRate:
                GameManager.Instance.playerData.goldBonusLevel++;
                break;
        }
        GameManager.Instance.clickHandler.UpdateAutoAttack();
    }

    public void UIRefresh(StatIndex index)
    {

        _name.text = $"{data.skillName} {CurrentLevel}";


        if (CurrentLevel == data.maxLevel)
        {
            btnText.text = "Max";
            btnText.color = Color.red;
        }
        else
        {
            btnText.text = $"{currentPrice}";
        }

        switch (index)
        {
            case StatIndex.AutoAttackInterval:
                float attackSpeed = 1.0f / (1 + CurrentLevel * 0.2f);
                description.text = $"{attackSpeed:F2}초마다 1번 공격";
                break;

            case StatIndex.GoldGainRate:
                description.text = $"{data.skillDescription} + {CurrentLevel * data.impressionStat}.0%";
                break;

            case StatIndex.CriticalDamage:
                description.text = $"{data.skillDescription} + {CurrentLevel * data.impressionStat}.0%";
                break;
        }
    }

    //업그레이드 버튼에 비용을 표시할 때, 재화가 충분한 경우에는 검은색, 재화가 부족한 경우에는 빨간 색으로 표시
    public void CheckEnoughCoins()
    {
        if (currentLevel == data.maxLevel)
        {
            btnText.color = Color.red;
        }
        //충분한 경우
        else if (GameManager.Instance.playerData.coin >= currentPrice)
        {
            btnText.color = Color.black;
        }
        //충분하지 않은 경우
        else
        {
            btnText.color = Color.red;
        }
    }


    private bool CheckMaxLevel(int value)
    {
        if (value > data.maxLevel)
        {
            //Debug.Log("이미 최대 레벨입니다.");
            return false;
        }

        return true;
    }

    public void OnPointerDown()
    {
        startTimer = true;
    }

    public void OnPointerUp()
    {
        startTimer = false;
        isHolding = false;
        timer = 0.0f;
    }

}
