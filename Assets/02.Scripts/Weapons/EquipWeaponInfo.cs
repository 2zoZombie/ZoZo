using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static Cinemachine.DocumentationSortingAttribute;

public class EquipWeaponInfo : MonoBehaviour
{
    //장착무기 정보
    public WeaponData weaponInfo;

    public TMP_Text equipName;
    public Image equipIcon;

    public TMP_Text equipATK;
    public TMP_Text equipCRIT;

    public void EquipData()
    {
        bool isPuchased = weaponInfo.isPurchased;

        if (isPuchased)
        {
            equipName.text = weaponInfo.weaponSO.weaponName;
            equipIcon.sprite = weaponInfo.weaponSO.weaponIcon;

            equipATK.text =
            $"공격력 : {weaponInfo.weaponSO.baseAttack + weaponInfo.weaponLevel * weaponInfo.weaponSO.attackValum_Up}";
            equipCRIT.text =
            $"치명타 확률 : {weaponInfo.weaponSO.baseCriticalChance} %";

        }
    }
}
