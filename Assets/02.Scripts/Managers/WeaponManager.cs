using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : Singleton<WeaponManager>
{

    public WeaponUI weaponUI;
    public WeaponSO WeaponSO;

    private void Awake()
    {
        base.Awake();
    }


}
