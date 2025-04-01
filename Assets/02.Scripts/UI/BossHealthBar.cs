using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealthBar : HealthBar
{
    protected override void FollowTarget()
    {

    }

    protected override void Die()
    {
        target = null;
        gameObject.SetActive(false);
    }
}
