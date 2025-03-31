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


    int stage = 0;
    public int spawncount;
    public int curspawncout;
    public Image uiBar;

    public List<GameObject> spawnEnemy = new List<GameObject>();

    [SerializeField] private TextMeshProUGUI SpawnCaout;

    Enemy enemy;

    public void Start()
    {
        StartCoroutine(CapterCoroutine());
        enemy = FindObjectOfType<Enemy>();
    }
    private void Update()
    {
        SetSpawnText();
        uiBar.fillAmount = GetPercentage();
    }

    private IEnumerator CapterCoroutine()
    {
        GameObject tlieMap;
        for (int i = 0; i < capterMap.Length;)
        {
            tlieMap = Instantiate(capterMap[i], transform.position, Quaternion.identity);
            Debug.Log("현재 챕터 : " + (i+1)); // UI 표시
            stage = 0;
            if (stage == 0)
            {
                for (int s = stage; s <= 4; s++)
                {
                    while (curspawncout == 0)
                    {
                        NextStage();
                    }
                    if (s == 4)
                    {
                        yield return new WaitUntil(() => GameObject.FindGameObjectsWithTag("Enemy").Length == 0);
                        yield return new WaitForSeconds(3f);
                        Destroy(tlieMap);
                        i++;
                        break;
                    }
                    yield return new WaitUntil(() => GameObject.FindGameObjectsWithTag("Enemy").Length == 0);
                    yield return new WaitForSeconds(3f);
                    stage++;
                    NextStage();
                }
            }
        }
    }

    public void SpawnEnemy()
    {
        int enmys = 3;
        for (int i = 0; i < enmys; i++)
        {
            enmys = 3;

            if (stage > 0)
            {
                enmys = enmys * (stage + 1);
            }
            Vector3 spawnPosition = new Vector3(Random.Range(2.5f, 3), Random.Range(-1f, 3f), 0);
            int num = Random.Range(0, 3);
            GameObject enemyObject;
            if (stage % 4 == 0 && stage != 0)
            {
                enemyObject = Instantiate(enemies[4].gameObject, spawnPosition, Quaternion.identity);
                enemyObject.name = "Boss Enemy";
                spawncount++;
                return;
            }
            else
            {
                enemyObject = Instantiate(enemies[num].gameObject, spawnPosition, Quaternion.identity);
                enemyObject.name = "Normal Enemy" + i;
                spawncount++;
            }
            spawnEnemy.Add(enemyObject);
            curspawncout = spawncount;
        }
    }


    private void SetSpawnText()
    {
        SpawnCaout.text = curspawncout + "/" + spawncount;
    }

    public void NextStage()
    {
        spawncount = 0;
        SpawnEnemy();
    }

    float GetPercentage()
    {
        return (curspawncout / spawncount) *100f;
    }
}

