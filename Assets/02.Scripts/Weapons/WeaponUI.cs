using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WeaponUI : MonoBehaviour
{
    WeaponManager weaponManager;
    [Header("WeaponInfo")]
    public TMP_Text TMP_WeaponName;
    public Image WeaponIcon;
    public TMP_Text TMP_AttackPower;
    public TMP_Text TMP_CriticalChance;

    public void Start()
    {
        weaponManager = WeaponManager.Instance;
    }

    public void Refresh()
    {

    }


}
