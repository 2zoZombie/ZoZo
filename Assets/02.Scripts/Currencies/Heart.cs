using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : DropItem
{


    protected override void GetItem()
    {
        GameManager.Instance.Heal(5);
    }
}
