using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.UI;

public class InvenPopup : MonoBehaviour
{
    WeaponManager weaponManager;
    public Dictionary<int, WeaponData> WeaponDataList = new Dictionary<int, WeaponData>();
    public List<InvenSlot> slots = new List<InvenSlot>();
    public Transform slotPanel;
    public GameObject invenPopup;
    public GameObject slotPrefab;
    private bool isInit;

    public void Init()
    {
        isInit = true;
        weaponManager = WeaponManager.Instance;
        //각 슬롯에 인덱스 부여
        for (int i = 0; i < weaponManager.weaponDatas.Count; i++)
        {
            InvenSlot slot = Instantiate(slotPrefab, slotPanel).GetComponent<InvenSlot>();
            slot.slotIndex = i;
            slot.SetData(weaponManager.weaponDatas[i]);
            slot.Init(this);
            slots.Add(slot);
            
        }
    }

    public void OninvenPopup()
    {
        if (isInit==false)
        {
            Init();
        }
        invenPopup.SetActive(!invenPopup.activeSelf);
        RefreshAllSlots();

    }

    public void RefreshAllSlots()
    {
        for (int i = 0; i < slots.Count; i++)
        {
            slots[i].RefreshSlot();
        }
    }
}
