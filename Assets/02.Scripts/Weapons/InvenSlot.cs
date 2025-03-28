using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InvenSlotInfo : MonoBehaviour
{
    //슬롯에 들어갈 정보와 변수
    [Header("WeaponInfo")]
    public WeaponSO WeaponSO;

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

    


    private void Start()
    {

    }

    public void SetData(WeaponSO weaponSO)
    {
        WeaponSO = weaponSO;
        WeaponIcon.sprite = WeaponSO.weaponIcon;
        WPName.text = WeaponSO.weaponName;
        WPLevel.text = "Lv.0";
        ATKVolum.text = WeaponSO.baseAttack.ToString();
        CRITVolum.text = WeaponSO.baseCriticalChance.ToString("N1") + "%";
        BuyCost.text = WeaponSO.buyCost.ToString();
        UpgradeCost.text = WeaponSO.upgradeCost.ToString();
    }

    public void OnBuyButton()
    {
        //구매 버튼 비활성화
        BuyButton.SetActive(false);
        //강화버튼,장착 버튼 활성화
        Equip_UpgradeBtn.SetActive(true);
        //장비 정보 불러오기
        


    }

    public void OnUpgradeButton()
    {

    }

    public void OnEquip()
    {
        
    }
}
