using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class InvenPopup : MonoBehaviour
{
    WeaponManager weaponManager;

    public InvenSlot[] slots;
    public Transform slotPanel;

    public void Start()
    {
        weaponManager = WeaponManager.Instance;

        slots = new InvenSlot[slotPanel.childCount];
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i] = slotPanel.GetChild(i).GetComponent<InvenSlot>();
            slots[i].slotIndex = i;
            slots[i].InvenPopup = this;
            
        }

    }


    public void Refresh()
    {

    }


}
