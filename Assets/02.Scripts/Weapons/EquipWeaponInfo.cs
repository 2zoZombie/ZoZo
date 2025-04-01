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
    public WeaponData equipedWeapon;

    public TMP_Text equipName;
    public Image equipIcon;

    public TMP_Text equipATK;
    public TMP_Text equipCRIT;

    private void Awake()
    {
        WeaponManager.Instance.equipWeaponInfo = this;
    }

    public void SetEquipData(WeaponData data)
    {
        equipedWeapon = data;

        equipName.text = equipedWeapon.weaponSO.weaponName;
        equipIcon.sprite = equipedWeapon.weaponSO.weaponIcon;

        equipATK.text =
        $"공격력 : {equipedWeapon.weaponSO.baseAttack + equipedWeapon.weaponLevel * equipedWeapon.weaponSO.attackVolume_Up}";
        equipCRIT.text =
        $"치명타 확률 : {equipedWeapon.weaponSO.baseCriticalChance} %";

    }
}
