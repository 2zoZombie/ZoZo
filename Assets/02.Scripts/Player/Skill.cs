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
            //CurrentLevel�� �ٲ� ������ playerData�� ���� ����
            currentLevel = value;
            GameManager.Instance.playerData.statLevel[indexNum] = currentLevel;

            //���� ����
            currentPrice *= data.impressionPrice;

            //UI ����
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

        //SkillOB�� �Ҵ���� �ʾҴٸ� ���� ���� ����ϰ� ���� ���� ����
        if (data == null)
        {
            Debug.Log($"{this.name}�� SkillOB�� �Ҵ���� �ʾҽ��ϴ�.");
            return;
        }

        //������ ��ư�� �޼��� �Ҵ�
        levelupBtn.onClick.AddListener(SkillLevelUp);

        //���� ���� �ʱ�ȭ
        currentPrice = data.basicPrice;
        indexNum = (int)data.index;
    }

    //TODO: �׽�Ʈ�� ���� �ڵ� (�������� ���������)
    private void Awake()
    {
        GameManager.Instance.playerData = new PlayerData();
    }

    private void Start()
    {
        //UI ����
        UIRefresh(data.index);

        //���� ������ �ε��� �÷��̾� �����ʹ�� �ʱ�ȭ
        currentLevel = GameManager.Instance.playerData.statLevel[indexNum];

        //TODO: �׽�Ʈ �ڵ� �����ϱ�
        GameManager.Instance.GetCoin(100000);
    }


    public void SkillLevelUp()
    {
        //�̹� �Ѱ� �����̶�� �������� �ʴ´�.
        if (!CheckMaxLevel(CurrentLevel)) return;

        //�÷��̾ ���� ���ݸ�ŭ�� ������ ������ �ִ��� üũ (false�� ����X)
        if (!GameManager.Instance.SpendCoin(currentPrice))
        {
            return;
        }

        //�ش� ���� ������ 1 ������Ŵ
        CurrentLevel++; 
    }

    public void UIRefresh(StatIndex index)
    {
        _name.text = $"{data.skillName} {CurrentLevel}";
        btnText.text = $"������ {currentPrice}��";

        //�ӽ� �ڵ� (GameManager�� ���淡 ���ʿ��� ������)
        UIManager.Instance.coinDisplayUI.SetCoinText();

        switch (index)
        {
            case StatIndex.AutoAttackInterval:
                //�ڵ� ���� �ڵ� ���� �����ϱ�
                description.text = $"{120 - CurrentLevel * data.impressionStat}�ʸ��� 1�� ����";
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
            Debug.Log("�̹� �ִ� �����Դϴ�.");
            return false;
        }
        else if (value == data.maxLevel - 1)
        {
            btnText.text = "MAX";
        }

        return true;
    }
}
