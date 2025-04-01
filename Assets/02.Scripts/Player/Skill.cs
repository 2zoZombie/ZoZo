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
            //�̹� �Ѱ� �����̶�� �������� �ʴ´�.
            if (!CheckMaxLevel(CurrentLevel)) return;

            //�÷��̾ ���� ���ݸ�ŭ�� ������ ������ �ִ��� üũ (false�� ����X)
            if (!GameManager.Instance.SpendCoin(currentPrice))
            {
                return;
            }

            //CurrentLevel�� �ٲ� ������ playerData�� ���� ����
            currentLevel = value;
            GameManager.Instance.playerData.statLevel[indexNum] = currentLevel;

            //���� ����
            currentPrice *= data.impressionPrice;

            //UI ����
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

        //�̺�Ʈ �Ҵ�
        trigger = transform.Find("LevelUpBtn")?.GetComponent<EventTrigger>();
        if (trigger == null)
        {
            transform.Find("LevelUpBtn")?.AddComponent<EventTrigger>();
        }

        //�̺�Ʈ �ٿ��ֱ�
        EventTrigger.Entry entryDown = new EventTrigger.Entry();
        EventTrigger.Entry entryUp = new EventTrigger.Entry();

        entryDown.eventID = EventTriggerType.PointerDown;
        entryUp.eventID = EventTriggerType.PointerUp;

        entryDown.callback.AddListener((data) => { OnPointerDown(); });
        entryUp.callback.AddListener((data) => { OnPointerUp(); });

        //OnValidate�� �ڲ� �������� �پ �߰���...
        trigger.triggers.Clear();

        trigger.triggers.Add(entryDown);
        trigger.triggers.Add(entryUp);

    }


    private void Start()
    {
        //UI ����
        UIRefresh(data.index);

        //���� ������ �ε��� �÷��̾� �����ʹ�� �ʱ�ȭ
        currentLevel = GameManager.Instance.playerData.statLevel[indexNum];
        GameManager.Instance.OnCoinChange += CheckEnoughCoins;
    }

    private void Update()
    {
        //���� ������ ���Ӱ�ȭ
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
                CurrentLevel++;
                timer = 0.0f;
            }
        }
    }

    public void SkillLevelUp()
    {
        //�ش� ���� ������ 1 ������Ŵ
        CurrentLevel++;
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
                description.text = $"{attackSpeed:F2}�ʸ��� 1�� ����";
                break;

            case StatIndex.GoldGainRate:
                description.text = $"{data.skillDescription} + {CurrentLevel * data.impressionStat}.0%";
                break;

            case StatIndex.CriticalDamage:
                description.text = $"{data.skillDescription} + {CurrentLevel * data.impressionStat}.0%";
                break;
        }
    }

    //���׷��̵� ��ư�� ����� ǥ���� ��, ��ȭ�� ����� ��쿡�� ������, ��ȭ�� ������ ��쿡�� ���� ������ ǥ��
    public void CheckEnoughCoins()
    {
        if (currentLevel == data.maxLevel)
        {
            btnText.color = Color.red;
        }
        //����� ���
        else if (GameManager.Instance.playerData.coin >= currentPrice)
        {
            btnText.color = Color.black;
        }
        //������� ���� ���
        else
        {
            btnText.color = Color.red;
        }
    }


    private bool CheckMaxLevel(int value)
    {
        if (value >= data.maxLevel)
        {
            Debug.Log("�̹� �ִ� �����Դϴ�.");
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
