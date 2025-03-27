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
        btnText.text = $"������ {data.basicPrice}��";
    }

    public void OnClickLevelUpBtn()
    {
        //level�� ���������ָ� �� (�Ʒ��� �׽�Ʈ �ڵ�)
        //��尡 ������� üũ���־�� ���� ���ӸŴ����� SpendCoin �޼��� ����� ��.


        //playerData ����
        //level ���� ���� �߰��ȴٸ� �� �ڵ嵵 �߰�
        GameManager.Instance.playerData.statLevel[((int)data.type)]++;
        
        //UI Refresh
        _name.text = $"{data.skillName} {GameManager.Instance.playerData.statLevel[((int)data.type)]}";
        description.text = $"{data.skillDescription} + {GameManager.Instance.playerData.statLevel[((int)data.type)] * data.impressionStat}.0%";

        //
        btnText.text = $"������ {data.basicPrice * (data.impressionPrice * GameManager.Instance.playerData.statLevel[((int)data.type)])}��";
    }    
}
