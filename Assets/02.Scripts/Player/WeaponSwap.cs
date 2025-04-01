using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwap : MonoBehaviour
{
    public SpriteRenderer weaponSprite;
    public Animator weaponAnim;

    private void OnValidate()
    {
        weaponSprite = GetComponent<SpriteRenderer>();
        weaponAnim = GetComponent<Animator>();
    }

    public void Equip(GameObject weaponPrepab)
    {
        weaponSprite.sprite = weaponPrepab.GetComponent<SpriteRenderer>().sprite;
        weaponAnim.runtimeAnimatorController = weaponPrepab.GetComponent<Animator>().runtimeAnimatorController;
    }


}
