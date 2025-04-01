using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [Header("HPbarUI")]
    public Image instantHpBar;
    public Image delayedHpBar;
    public TextMeshProUGUI entityName;
    protected float targetFill;
    protected float delaySpeed = 1f;

    protected RectTransform hpBarUI;

    [Header("Target")]
    public Entity target;
    public Vector3 offset = new Vector3(0, 1f, 0);  


    protected virtual void Awake()
    {
        hpBarUI = this.GetComponent<RectTransform>();
       gameObject.SetActive(false);
    }

    private void Update()
    {
        if (target == null) return;

        FollowTarget();
    }

    protected virtual void FollowTarget()
    {
        Vector3 screenPos = Camera.main.WorldToScreenPoint(target.transform.position + offset);
        screenPos.z = 0;
        transform.position = screenPos;
    }

    public void SetTarget(Entity target)
    {
        this.target = target;
        entityName.text = target.entityName;
        instantHpBar.fillAmount = 1f;
        delayedHpBar.fillAmount = 1f;
    }

    public virtual void OnHit()
    {
        UpdateHP();

        instantHpBar.fillAmount = targetFill;
        delayedHpBar.DOKill(); 
        delayedHpBar.DOFillAmount(targetFill, delaySpeed).SetEase(Ease.OutQuad);

        if (target.curHp <= 0) Invoke("Die", 1f);//current hp가 0일경우 1초뒤 setactive false
    }

    public virtual void UpdateHP()
    {
        if (target == null) return;
        //entity와 연결 후 current hp max hp 받아오기
        targetFill = target.curHp / target.maxHp;
    }

    protected virtual void Die()
    {
        target = null;
        UIManager.Instance.healthBarPool.ReturnToPool(this.gameObject);
    }

    public void Revive(float duration)
    {
        instantHpBar.DOFillAmount(1f, duration).SetEase(Ease.Linear);
    }
}
