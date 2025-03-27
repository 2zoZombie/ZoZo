using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Timeline.TimelinePlaybackControls;

public class Skill : MonoBehaviour
{
    public SkillSO data;
    public Image skillImage;

    public Button levelupBtn;
    public TextMeshProUGUI btnText;
    public TextMeshProUGUI _name;
    public TextMeshProUGUI description;

    private void OnValidate()
    {
        skillImage = transform.Find("Icon")?.GetComponent<Image>();
        levelupBtn = transform.Find("LevelUpBtn")?.GetComponent<Button>();
        btnText = levelupBtn.GetComponentInChildren<TextMeshProUGUI>();


        _name = transform.Find("Name")?.GetComponent<TextMeshProUGUI>();
        description = transform.Find("Description")?.GetComponent<TextMeshProUGUI>();

        levelupBtn.onClick.AddListener(OnClickLevelUpBtn);
        skillImage.sprite = data.Icon;

    }

    private void Start()
    {
        //UI Refresh
        _name.text = $"{data.skillName} {GameManager.Instance.playerData.statLevel[((int)data.type)]}";
        description.text = $"{data.skillDescription} + {GameManager.Instance.playerData.statLevel[((int)data.type)] * data.impressionStat}.0%";
        btnText.text = $"레벨업 {data.basicPrice}ⓒ";
    }

    public void OnClickLevelUpBtn()
    {
        //level만 증가시켜주면 됨 (아래는 테스트 코드)
        //골드가 충분한지 체크해주어야 때는 게임매니저의 SpendCoin 메서드 사용할 것.


        //playerData 수정
        //level 말고 스탯 추가된다면 그 코드도 추가
        GameManager.Instance.playerData.statLevel[((int)data.type)]++;
        
        //UI Refresh
        _name.text = $"{data.skillName} {GameManager.Instance.playerData.statLevel[((int)data.type)]}";
        description.text = $"{data.skillDescription} + {GameManager.Instance.playerData.statLevel[((int)data.type)] * data.impressionStat}.0%";

        //
        btnText.text = $"레벨업 {data.basicPrice * (data.impressionPrice * GameManager.Instance.playerData.statLevel[((int)data.type)])}ⓒ";
    }    
}
