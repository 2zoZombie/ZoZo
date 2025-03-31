using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StageUI : MonoBehaviour
{
    EnemyManager enemyManager;
    [SerializeField] private TextMeshProUGUI SpawnCaout;
    private float curCount;
    private float startCount;



    private void Start()
    {
        enemyManager = GetComponent<EnemyManager>();

        SetSpawCount();
        SetSpawnText();
    }
    private void Update()
    {
        //uiBar.fillAmount = GetPercentage();
    }

    //float GetPercentage()
    //{
    //    return curCount / startCount;
    //}
    private void SetSpawCount()
    {
        startCount = enemyManager.spawncount;
        curCount = enemyManager.curspawncout;
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
