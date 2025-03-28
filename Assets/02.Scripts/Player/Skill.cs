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

    //자동 공격 속도 스탯이 따로 없어서 만드는 임시 변수
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
        btnText.text = $"레벨업 {currentPrice}ⓒ";

        //TODO: 테스트 코드 삭제하기
        GameManager.Instance.GetCoin(1000);
    }

    public void LevelUpPercentageBuff()
    {
        //TODO: 테스트 코드 삭제하기
        Debug.Log("플레이어 코인: " + GameManager.Instance.playerData.coin);

        //플레이어가 현재 가격만큼의 코인을 가지고 있는지 체크
        if (!GameManager.Instance.SpendCoin(currentPrice))
        {
            return;
        }

        //가격 갱신
        currentPrice *= data.impressionPrice;

        //해당 스텟 레벨을 1 증가시킴
        GameManager.Instance.playerData.statLevel[((int)data.index)]++;

        //UI Refresh
        UIRefresh(data.type);
    }

    public void LevelUpTimedActive()
    {
        //TODO: 테스트 코드 삭제하기
        Debug.Log("플레이어 코인: " + GameManager.Instance.playerData.coin);

        //플레이어가 현재 가격만큼의 코인을 가지고 있는지 체크
        if (!GameManager.Instance.SpendCoin(currentPrice))
        {
            return;
        }

        //가격 갱신
        currentPrice *= data.impressionPrice;

        //해당 스텟 레벨을 1 증가시킴
        GameManager.Instance.playerData.statLevel[((int)data.index)]++;

        //UI Refresh
        UIRefresh(data.type);
    }

    public void UIRefresh(SkillType type)
    {
        _name.text = $"{data.skillName} {GameManager.Instance.playerData.statLevel[((int)data.index)]}";
        btnText.text = $"레벨업 {currentPrice}ⓒ";

        switch (type)
        {
            case SkillType.PercentageBuff:
                description.text = $"{data.skillDescription} + {GameManager.Instance.playerData.statLevel[((int)data.index)] * data.impressionStat}.0%";
                break;

            case SkillType.TimedActive:
                description.text = $"{GameManager.Instance.playerData.statLevel[((int)data.index)] * data.impressionStat}초마다 1번 공격";
                break;

        }
    }
}
