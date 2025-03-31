using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.InputSystem.XR;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class EnemyManager : Singleton<EnemyManager>
{
    [SerializeField] public Enemy[] enemies;
    [SerializeField] GameObject[] capterMap;


    int stage;
    public int spawncount;
    public int curspawncout;
    public Image uiBar;

    public List<GameObject> spawnEnemy = new List<GameObject>();

    [SerializeField] private TextMeshProUGUI SpawnCaoutText;
    [SerializeField] private TextMeshProUGUI ChapterText;

    [SerializeField] private Slider stageSlider;
    private int currentstage;

    Enemy enemy;

    public void Start()
    {
        StartCoroutine(CapterCoroutine());
        enemy = FindObjectOfType<Enemy>();
        stageSlider.minValue = 0;
        stageSlider.maxValue = 5;
        stageSlider.value = currentstage;
    }
    private void Update()
    {
        SetSpawnText();
        stage = 0;
        //uiBar.fillAmount = GetPercentage();
    }

    public IEnumerator CapterCoroutine()
    {
        GameObject tlieMap;
        for (int i = 0; i < capterMap.Length; i++)
        {
            tlieMap = Instantiate(capterMap[i], transform.position, Quaternion.identity);
            currentstage = 0; 
            stageSlider.value = currentstage;
            for (int s = currentstage; s < 5; s++)
            {
                ChapterText.text = ($"Chpter : {i+1}");

                spawncount = 0;
                SpawnEnemy();
                yield return new WaitUntil(() => GameObject.FindGameObjectsWithTag("Enemy").Length == 0);
                yield return new WaitForSeconds(3f);
                if (s == 4)
                {
                    Destroy(tlieMap);
                    break;
                }
                currentstage++;
                //enemy.GrowthStats();
                SetStage();
            }
            stage = 0;
            //enemy.BossGrowthStats();
        }
    }

    public void SpawnEnemy()
    {
        int enmys = 3;
        for (int i = 0; i < enmys; i++)
        {
            enmys = 3;
            if (currentstage > 0)
            {
                enmys = enmys * (currentstage+1) ;
            }
            Vector3 spawnPosition = new Vector3(Random.Range(2.5f, 3), Random.Range(-1f, 3f), 0);
            int num = Random.Range(0, 3);
            GameObject enemyObject;
            if (currentstage % 4 == 0 && currentstage != 0)
            {
                enemyObject = Instantiate(enemies[4].gameObject, spawnPosition, Quaternion.identity);
                enemyObject.name = "Boss Enemy";
                spawncount++;
                curspawncout = spawncount;
                break;
            }
            else
            {
                enemyObject = Instantiate(enemies[(num)].gameObject, spawnPosition, Quaternion.identity);
                enemyObject.name = "Normal Enemy" + i;
                spawncount++;
                curspawncout = spawncount;
            }
            spawnEnemy.Add(enemyObject);
        }
    }

    private void SetStage()
    {
        stageSlider.value = currentstage;
        stageSlider.minValue = 0;
        stageSlider.maxValue = 4;
    }

    private void SetSpawnText()
    {
        SpawnCaoutText.text = curspawncout + "/" + spawncount;
    }

    public void NextStage()
    {
        //enemy.SetStats();
        SpawnEnemy();
    }

    //float GetPercentage()
    //{
    //    uiBar.fillAmount = 1f;
    //    return (curspawncout / spawncount)f;
    //}
}

