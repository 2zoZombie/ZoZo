using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.UI;

public class InvenSlot : MonoBehaviour
{
    WeaponManager weaponManager;

    //슬롯에 들어갈 정보와 변수
    [Header("WeaponInfo")]
    public WeaponSO WeaponSO;

    //정보가 들어갈 자리
    [Header("SlotInfo")]
    public int slotIndex;
    public bool equipped;

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
        
        //슬롯 별 구매 코스트
        BuyCost.text = weaponManager.weaponSOList[slotIndex].buyCost.ToString("N0");
    }

    public void Refresh()
    {
        SetData(weaponManager.weaponDatas[slotIndex]);
    }

    public void SetData(WeaponData data)
    {
        bool isPuchased = data.isPurchased;
        WeaponSO = data.weaponSO;// 아래 if문으로 처리
        

        if (isPuchased)
        {
            WeaponIcon.sprite = WeaponSO.weaponIcon;
            WPName.text = WeaponSO.weaponName;
            WPLevel.text = "Lv." + $"{data.weaponLevel}";
            ATKVolum.text = WeaponSO.baseAttack.ToString();
            CRITVolum.text = WeaponSO.baseCriticalChance.ToString("N1") + "%";
            UpgradeCost.text = WeaponSO.upgradeCost.ToString();
        }
    }

    public void OnBuyButton()
    {
        //구매 버튼 비활성화
        BuyButton.SetActive(false);
        //강화버튼,장착 버튼 활성화
        Equip_UpgradeBtn.SetActive(true);

        //구매 여부
        weaponManager.weaponDatas[slotIndex].isPurchased = true;

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
        //장착 버튼 비활성화
        EquipButton.SetActive(!EquipButton.activeSelf);
        weaponManager.EquipWeapon();
    }
}
