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
        //�� ���Կ� �ε��� �ο�
        slots = new InvenSlot[slotPanel.childCount];
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i] = slotPanel.GetChild(i).GetComponent<InvenSlot>();
            slots[i].slotIndex = i;
        }
    }

    public void WeaponDataSet()
    {
        //���ҽ� ������ ���� �������� ����
        Debug.Log("���濡 ���⼼�� �Ϸ�");
    }

    public void Refresh()
    {

    }


}
