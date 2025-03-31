using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropItemPool : ObjectPool<DropItem>
{
    protected override void Awake()
    {
        base.Awake();
        GameManager.Instance.dropItemPool = this;
    }
}
