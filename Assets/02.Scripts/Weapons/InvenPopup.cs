using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.Profiling.Memory.Experimental;
using UnityEngine.UI;

public class InvenPopup : MonoBehaviour
{
    WeaponManager weaponManager;
    public Dictionary<int, WeaponData> WeaponDataList = new Dictionary<int, WeaponData>();
    public List<InvenSlot> slots = new List<InvenSlot>();
    public Transform slotPanel;
    public GameObject invenPopup;
    public GameObject slotPrefab;

    private void Awake()
    {
        weaponManager = WeaponManager.Instance;

    }

    public void Start()
    {
        //각 슬롯에 인덱스 부여
        for (int i = 0; i < weaponManager.weaponDatas.Count; i++)
        {
            InvenSlot slot = Instantiate(slotPrefab, slotPanel).GetComponent<InvenSlot>();
            slot.slotIndex = i;
            slot.SetData(weaponManager.weaponDatas[i]);
            slots.Add(slot);
        }
    }

    public void WeaponDataSet()
    {
        //리소스 폴더의 무기 정보들을 저장
        Debug.Log("가방에 무기세팅 완료");
    }

    public void OninvenPopup()
    {
        invenPopup.SetActive(!invenPopup.activeSelf);  
    }

}
