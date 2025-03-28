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

    public int currentPrice = 0;

    //�ڵ� ���� �ӵ� ������ ���� ��� ����� �ӽ� ����
    public float speed = 120f;


    private void OnValidate()
    {
        skillImage = transform.Find("Icon")?.GetComponent<Image>();
        levelupBtn = transform.Find("LevelUpBtn")?.GetComponent<Button>();
        btnText = levelupBtn.GetComponentInChildren<TextMeshProUGUI>();

        _name = transform.Find("Name")?.GetComponent<TextMeshProUGUI>();
        description = transform.Find("Description")?.GetComponent<TextMeshProUGUI>();

        skillImage.sprite = data.Icon;

        if (data == null) return;

        switch(data.type)
        {
            case SkillType.PercentageBuff:
                levelupBtn.onClick.AddListener(LevelUpPercentageBuff);
                break;

            case SkillType.TimedActive:
                levelupBtn.onClick.AddListener(LevelUpTimedActive);
                break;
        }
    }

    private void Start()
    {
        currentPrice = data.basicPrice;

        //UI Refresh
        _name.text = $"{data.skillName} {GameManager.Instance.playerData.statLevel[((int)data.index)]}";
        description.text = $"{data.skillDescription} + {GameManager.Instance.playerData.statLevel[((int)data.index)] * data.impressionStat}.0%";
        btnText.text = $"������ {currentPrice}��";

        //TODO: �׽�Ʈ �ڵ� �����ϱ�
        GameManager.Instance.GetCoin(1000);
    }

    public void LevelUpPercentageBuff()
    {
        //TODO: �׽�Ʈ �ڵ� �����ϱ�
        Debug.Log("�÷��̾� ����: " + GameManager.Instance.playerData.coin);

        //�÷��̾ ���� ���ݸ�ŭ�� ������ ������ �ִ��� üũ
        if (!GameManager.Instance.SpendCoin(currentPrice))
        {
            return;
        }

        //���� ����
        currentPrice *= data.impressionPrice;

        //�ش� ���� ������ 1 ������Ŵ
        GameManager.Instance.playerData.statLevel[((int)data.index)]++;

        //UI Refresh
        UIRefresh(data.type);
    }

    public void LevelUpTimedActive()
    {
        //TODO: �׽�Ʈ �ڵ� �����ϱ�
        Debug.Log("�÷��̾� ����: " + GameManager.Instance.playerData.coin);

        //�÷��̾ ���� ���ݸ�ŭ�� ������ ������ �ִ��� üũ
        if (!GameManager.Instance.SpendCoin(currentPrice))
        {
            return;
        }

        //���� ����
        currentPrice *= data.impressionPrice;

        //�ش� ���� ������ 1 ������Ŵ
        GameManager.Instance.playerData.statLevel[((int)data.index)]++;

        //UI Refresh
        UIRefresh(data.type);
    }

    public void UIRefresh(SkillType type)
    {
        _name.text = $"{data.skillName} {GameManager.Instance.playerData.statLevel[((int)data.index)]}";
        btnText.text = $"������ {currentPrice}��";

        switch (type)
        {
            case SkillType.PercentageBuff:
                description.text = $"{data.skillDescription} + {GameManager.Instance.playerData.statLevel[((int)data.index)] * data.impressionStat}.0%";
                break;

            case SkillType.TimedActive:
                description.text = $"{GameManager.Instance.playerData.statLevel[((int)data.index)] * data.impressionStat}�ʸ��� 1�� ����";
                break;

        }
    }
}
