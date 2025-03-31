using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Skill : MonoBehaviour
{
    public SkillSO data;
    public Image skillImage;

    public Button levelupBtn;
    public TextMeshProUGUI btnText;
    public TextMeshProUGUI _name;
    public TextMeshProUGUI description;

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
            //CurrentLevel이 바뀔 때마다 playerData의 값을 변경
            currentLevel = value;
            GameManager.Instance.playerData.statLevel[indexNum] = currentLevel;

            //가격 갱신
            currentPrice *= data.impressionPrice;

            //UI 갱신
            UIRefresh(data.index);
        }
    }


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
    }

    //TODO: 테스트를 위한 코드 (합쳐지면 지워줘야함)
    private void Awake()
    {
        GameManager.Instance.playerData = new PlayerData();
    }

    private void Start()
    {
        //UI 갱신
        UIRefresh(data.index);

        //현재 레벨을 로드한 플레이어 데이터대로 초기화
        currentLevel = GameManager.Instance.playerData.statLevel[indexNum];

        //TODO: 테스트 코드 삭제하기
        GameManager.Instance.GetCoin(100000);
    }


    public void SkillLevelUp()
    {
        //이미 한계 레벨이라면 변경하지 않는다.
        if (!CheckMaxLevel(CurrentLevel)) return;

        //플레이어가 현재 가격만큼의 코인을 가지고 있는지 체크 (false시 실행X)
        if (!GameManager.Instance.SpendCoin(currentPrice))
        {
            return;
        }

        //해당 스텟 레벨을 1 증가시킴
        CurrentLevel++; 
    }

    public void UIRefresh(StatIndex index)
    {
        _name.text = $"{data.skillName} {CurrentLevel}";
        btnText.text = $"레벨업 {currentPrice}ⓒ";

        //임시 코드 (GameManager에 없길래 이쪽에서 갱신함)
        UIManager.Instance.coinDisplayUI.SetCoinText();

        switch (index)
        {
            case StatIndex.AutoAttackInterval:
                //자동 공격 코드 보고 수정하기
                description.text = $"{120 - CurrentLevel * data.impressionStat}초마다 1번 공격";
                break;

            case StatIndex.GoldGainRate:
                description.text = $"{data.skillDescription} + {CurrentLevel * data.impressionStat}.0%";
                break;

            case StatIndex.CriticalDamage:
                description.text = $"{data.skillDescription} + {CurrentLevel * data.impressionStat}.0%";
                break;
        }
    }

    private bool CheckMaxLevel(int value)
    {
        if (value >= data.maxLevel)
        {
            Debug.Log("이미 최대 레벨입니다.");
            return false;
        }
        else if (value == data.maxLevel - 1)
        {
            btnText.text = "MAX";
        }

        return true;
    }
}
