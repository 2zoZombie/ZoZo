using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : Singleton<WeaponManager>
{
    public List<ScriptableObject> WeaponList = new List<ScriptableObject>();
    public InvenSlotInfo[] slots;

    public GameObject InvenPopup;



    protected override void Awake()
    {
        base.Awake();
    }


}
