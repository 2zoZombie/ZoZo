using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InvenSlot : MonoBehaviour
{
    WeaponManager weaponManager;

    //���Կ� �� ������ ����
    [Header("WeaponInfo")]
    public WeaponSO WeaponSO;
    public InvenPopup InvenPopup;

    //������ �� �ڸ�
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

    //������ ��ư ������Ʈ
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
        //�� ���Կ� �ε��� �ο�
        slots = new InvenSlot[slotPanel.childCount];
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i] = slotPanel.GetChild(i).GetComponent<InvenSlot>();
            slots[i].slotIndex = i;
        }

        //���� �� ���� �ڽ�Ʈ
        BuyCost.text = weaponManager.WeaponList[slotIndex].buyCost.ToString("N0");
    }

    public void SetData(WeaponSO weaponSO)
    {
        WeaponSO = weaponSO;
        WeaponIcon.sprite = WeaponSO.weaponIcon;
        WPName.text = WeaponSO.weaponName;
        WPLevel.text = "Lv."+ $"{WPLevel}";
        ATKVolum.text = WeaponSO.baseAttack.ToString();
        CRITVolum.text = WeaponSO.baseCriticalChance.ToString("N1") + "%";
        UpgradeCost.text = WeaponSO.upgradeCost.ToString();
    }

    public void OnBuyButton()
    {
        //���� ��ư ��Ȱ��ȭ
        BuyButton.SetActive(false);
        //��ȭ��ư,���� ��ư Ȱ��ȭ
        Equip_UpgradeBtn.SetActive(true);
        //��� ���� �ҷ�����
        SetData(weaponManager.WeaponList[slotIndex]);
        //�ڽ�Ʈ �Ҹ��ϱ�
    }

    public void OnUpgradeButton()
    {
        //���ⷹ����
        //�ڽ�Ʈ �Ҹ��ϱ�
    }

    public void OnEquip()
    {
        //������ ������ ���� ����
        //���� ���� ���� ����
        
    }
}
