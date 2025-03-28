using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InvenSlotInfo : MonoBehaviour
{
    //���Կ� �� ������ ����
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

    //������ ��ư ������Ʈ
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
        //���� ��ư ��Ȱ��ȭ
        BuyButton.SetActive(false);
        //��ȭ��ư,���� ��ư Ȱ��ȭ
        Equip_UpgradeBtn.SetActive(true);
        //��� ���� �ҷ�����
        


    }

    public void OnUpgradeButton()
    {

    }

    public void OnEquip()
    {
        
    }
}
