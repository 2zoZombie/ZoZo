using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [Header("HPbarUI")]
    public Image instantHpBar; 
    public Image delayedHpBar; 
    private float targetFill;
    private float delaySpeed = 1f;

    protected RectTransform hpBarUI;


    protected virtual void Start()
    {
        hpBarUI = this.GetComponent<RectTransform>();
        gameObject.SetActive(false);
    }

    public void OnHit()
    {
        UpdateHP();

        instantHpBar.fillAmount = targetFill;
        delayedHpBar.fillAmount = Mathf.Lerp(delayedHpBar.fillAmount, targetFill, Time.deltaTime * delaySpeed);
        //f(false) Die();//current hp가 0일경우 1초뒤 setactive false
    }

    public void UpdateHP()
    {
        //entity와 연결 후 current hp max hp 받아오기
        //targetFill = current / max;
    }

    void Die()
    {
        
    }
}
