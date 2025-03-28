using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InvenWeaponInfo : MonoBehaviour
{
    [Header("WeaponInfo")]
    public WeaponSO WeaponSO;
    public Image WeaponIcon;
    public TMP_Text WPName;
    public TMP_Text WPLevel;
    public TMP_Text ATKVolum;
    public TMP_Text CRITVolum;
    public TMP_Text BuyCost;
    public TMP_Text UpgradeCost;

    private void Awake()
    {
        WeaponIcon.sprite = WeaponSO.weaponIcon;
        WPName.text = WeaponSO.weaponName+" Lv.";
        ATKVolum.text = WeaponSO.baseAttack.ToString();
        CRITVolum.text = WeaponSO.baseCriticalChance.ToString("N1")+"%";
        BuyCost.text = WeaponSO.buyCost.ToString();
        UpgradeCost.text = WeaponSO.upgradeCost.ToString();
    }
}
