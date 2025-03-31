using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InvenSlot : MonoBehaviour
{
    WeaponManager weaponManager;

    //슬롯에 들어갈 정보와 변수
    [Header("WeaponInfo")]
    public WeaponSO WeaponSO;
    public InvenPopup InvenPopup;

    //정보가 들어갈 자리
    [Header("SlotInfo")]
    public int slotIndex;
    public bool equipped;
    public InvenSlot[] slots;
    public Transform slotPanel;

    public Image WeaponIcon;
    public TMP_Text WPName;
    public TMP_Text WPLevel;
    public TMP_Text ATKVolum;
    public TMP_Text CRITVolum;
    public TMP_Text BuyCost;
    public TMP_Text UpgradeCost;

    //슬롯의 버튼 오브젝트
    [Header("Button")]
    public GameObject BuyButton;
    public GameObject Equip_UpgradeBtn;
    public GameObject UpgradeButton;
    public GameObject EquipButton;

    private void Awake()
    {
        weaponManager = WeaponManager.Instance;
    }

    private void Start()
    {
        //각 슬롯에 인덱스 부여
        slots = new InvenSlot[slotPanel.childCount];
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i] = slotPanel.GetChild(i).GetComponent<InvenSlot>();
            slots[i].slotIndex = i;
        }

        //슬롯 별 구매 코스트
        BuyCost.text = weaponManager.WeaponSOList[slotIndex].buyCost.ToString("N0");
    }

    public void SetData(WeaponData data)
    {
        bool isPuchased = data.isPurchased;
        WeaponSO = data.weaponSO;// 아래 if문으로 처리
        WeaponIcon.sprite =  WeaponSO.weaponIcon;
        WPName.text = WeaponSO.weaponName;
        WPLevel.text = "Lv."+ $"{data.weaponLevel}";
        ATKVolum.text = WeaponSO.baseAttack.ToString();
        CRITVolum.text = WeaponSO.baseCriticalChance.ToString("N1") + "%";
        UpgradeCost.text = WeaponSO.upgradeCost.ToString();
    }

    public void OnBuyButton()
    {
        //구매 버튼 비활성화
        BuyButton.SetActive(false);
        //강화버튼,장착 버튼 활성화
        Equip_UpgradeBtn.SetActive(true);
        //장비 정보 불러오기
        SetData(weaponManager.weaponDatas[slotIndex]);
        //코스트 소모하기
    }

    public void OnUpgradeButton()
    {
        //무기레벨업
        //코스트 소모하기
    }

    public void OnEquip()
    {
        //기존의 장착된 무기 해제
        //현재 누른 무기 장착
        
    }
}
