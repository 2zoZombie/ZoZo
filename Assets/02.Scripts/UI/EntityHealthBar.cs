using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityHealthBar : HealthBar
{
    [Header("Position")]
    public GameObject entity;
    protected Transform target;
    public Vector3 offset = new Vector3(0f, 1f, 0);
    public Camera mainCamera;

    protected override void Start()
    {
        base.Start();
        mainCamera = Camera.main;
    }

    protected virtual void LateUpdate()
    {
        FollowTarget();
    }

    public void SetTarget(GameObject targetObj)
    {
        entity = targetObj;
        target = entity.transform;
    }

    public void FollowTarget()
    {
        if (target == null) return;

        Vector2 worldPos = target.position + offset;
        Vector2 screenPos = mainCamera.WorldToScreenPoint(worldPos);

        hpBarUI.position = screenPos;
    }
}
