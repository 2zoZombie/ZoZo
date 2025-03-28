using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StageUI : MonoBehaviour
{
    EnemyManager enemyManager;
    //[SerializeField] private TextMeshProUGUI damageText;
    [SerializeField] private TextMeshProUGUI SpawnCaout;
    private float curCount;
    private float startCount;


    //[SerializeField] TextMeshProUGUI capternumberText;
    public Image uiBar;
    private void Start()
    {
        enemyManager = GetComponent<EnemyManager>();

        SetSpawCount();
        SetSpawnText();
    }
    private void Update()
    {
        uiBar.fillAmount = GetPercentage();
    }

    //public void ShowDamageUI(float damage)
    //{
    //    TextMeshProUGUI damagePopup = Instantiate(damageText, gameObject.transform.position, Quaternion.identity);
    //    damageText.text = damage.ToString();
    //    damagePopup.transform.Translate(Vector3.up * 0.8f);
    //    Destroy(damageText.gameObject, 2f);
    //}

    float GetPercentage()
    {
        return curCount / startCount;
    }
    private void SetSpawCount()
    {
        startCount = 
        curCount = enemyManager.spawncount;
    }
    private void SetSpawnText()
    {
        SpawnCaout.text = curCount + "/" + startCount;
    }

    //private void Chapternum()
    //{
    //    capternumberUI.text = i + "/" + s;
    //}
}
