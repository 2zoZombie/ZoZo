using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.UI;

public class InvenSlot : MonoBehaviour
{
    WeaponManager weaponManager;
    InvenPopup invenPopup;

    //슬롯에 들어갈 정보와 변수
    [Header("WeaponInfo")]
    public WeaponData weaponData;
    public WeaponSO weaponSO;
    

    //정보가 들어갈 자리
    [Header("SlotInfo")]
    public int slotIndex;

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

    public void Init(InvenPopup inven)
    {
        invenPopup = inven;
        weaponManager = WeaponManager.Instance;
        //슬롯 별 구매 코스트
        BuyCost.text = weaponManager.weaponSOList[slotIndex].buyCost.ToString("N0");
    }

    public void SetData(WeaponData data)
    {
        bool isPuchased = data.isPurchased;
        weaponData = data;
        weaponSO = data.weaponSO;// 아래 if문으로 처리


        if (isPuchased)
        {
            WeaponIcon.sprite = weaponSO.weaponIcon;
            WPName.text = weaponSO.weaponName;
            WPLevel.text = "Lv." + $"{data.weaponLevel}";
            ATKVolum.text = CalculateATK().ToString();
            CRITVolum.text = weaponSO.baseCriticalChance.ToString("N1") + "%";
            UpgradeCost.text = weaponSO.upgradeCost.ToString();
        }
    }

    int CalculateATK()
    {
        return weaponSO.baseAttack + weaponSO.attackVolume_Up * weaponData.weaponLevel;
    }

    public void OnBuyButton()
    {
        if (weaponData == null) return;

        if (GameManager.Instance.SpendBlueCoin(weaponSO.buyCost))
        {
            //구매 여부
            weaponData.isPurchased = true;

            //장비 정보 불러오기
            SetData(weaponData);
            //코스트 소모하기

            RefreshSlot();
        }
    }

    public void OnUpgradeButton()
    {
        if (weaponData == null) return;

        if (GameManager.Instance.SpendBlueCoin(CalculateCost()))
        {
            weaponData.weaponLevel++; //무기레벨업
            weaponManager.equipWeaponInfo.SetEquipData(weaponData);//Setequipdata해주기
            SetData(weaponData);
        }//코스트 소모하기
    }

    int CalculateCost()
    {
        return weaponSO.upgradeCost + weaponSO.upgradeCost * weaponData.weaponLevel;
    }

    public void OnEquip()
    {
        if (weaponData == null) return;

        weaponData.isEquip = true;
        weaponManager.equipWeaponInfo.equipedWeapon.isEquip = false;
        weaponManager.equipWeaponInfo.SetEquipData(weaponData);
        invenPopup.RefreshAllSlots();

    }

    public void RefreshSlot()
    {
        //장착 여부
        EquipButton.SetActive(!weaponData.isEquip);
        //구매 여부
        BuyButton.SetActive(!weaponData.isPurchased);
        //강화 여부
        UpgradeButton.SetActive(weaponData.isPurchased);
        //강화/구매 버튼
        Equip_UpgradeBtn.SetActive(weaponData.isPurchased);
    }
}
