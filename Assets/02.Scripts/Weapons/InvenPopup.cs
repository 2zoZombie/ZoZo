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
    public InvenSlot[] slots;
    public Transform slotPanel;

    private void Awake()
    {
        weaponManager = WeaponManager.Instance;

    }

    public void Start()
    {
        //각 슬롯에 인덱스 부여
        slots = new InvenSlot[slotPanel.childCount];
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i] = slotPanel.GetChild(i).GetComponent<InvenSlot>();
            slots[i].slotIndex = i;
        }
    }

    public void WeaponDataSet()
    {
        //리소스 폴더의 무기 정보들을 저장
        Debug.Log("가방에 무기세팅 완료");
    }

    public void Refresh()
    {

    }


}
