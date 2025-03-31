using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.InputSystem.XR;
using UnityEngine.Rendering;

public class EnemyManager : Singleton<EnemyManager>
{
    [SerializeField] EnemyStatsTable enemyStatsTable;
    public List<Entity> enemies;
    [SerializeField] GameObject[] capterMap;


    int stage = 0;
    public int spawncount;

    private List<GameObject> spawnEnemy = new List<GameObject>();
    Enemy enemy;

    public void Start()
    {
        //StartCoroutine(CapterCoroutine());
    }

    /*private IEnumerator CapterCoroutine()
    {
        GameObject tlieMap;
        for (int i = 0; i < capterMap.Length;)
        {
            tlieMap = Instantiate(capterMap[i], transform.position, Quaternion.identity);
            Debug.Log("현재 챕터 : " + (i + 1)); // UI 표시
            stage = 0;
            if (stage == 0)
            {
                for (int s = stage; s <= 4; s++)
                {
                    while (spawncount == 0)
                    {
                        NextStage();
                    }
                    if (s == 4)
                    {
                        yield return new WaitUntil(() => GameObject.FindGameObjectsWithTag("Enemy").Length == 0);
                        yield return new WaitForSeconds(3f);
                        Destroy(tlieMap);
                        spawncount = 0;
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
    }*/

    public void SpawnEnemy(StageType stageType)
    {
        Entity enemy;

        switch (stageType)
        {
            case StageType.Normal:
                int enenmyQuantity = 3 + Random.Range(0, StageManager.Instance.currentStage + 1);
                for (int i = 0; i < enenmyQuantity; i++)
                {
                    enemy = Instantiate(
                        enemyStatsTable.enemyStatsList[Random.Range(0, enemyStatsTable.enemyStatsList.Count)].prefab,
                        RandomSpawnPosition(), Quaternion.identity).GetComponent<Entity>();

                    enemies.Add(enemy);
                }
                break;
            case StageType.Boss:

                enemy = Instantiate(
                    enemyStatsTable.bossStatsList[Random.Range(0, enemyStatsTable.bossStatsList.Count)].prefab,
                    RandomSpawnPosition(), Quaternion.identity).GetComponent<Entity>();

                enemies.Add(enemy);

                break;
            case StageType.Treasure:
                enemy = Instantiate(
                    enemyStatsTable.bossStatsList[Random.Range(0, enemyStatsTable.bossStatsList.Count)].prefab,
                    RandomSpawnPosition(), Quaternion.identity).GetComponent<Entity>();

                enemies.Add(enemy);
                break;
        }

        /*for (int i = 0; i < enmys; i++)
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
                enemyObject = Instantiate(enemies[3].gameObject, spawnPosition, Quaternion.identity);
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
        }*/


    }

    Vector3 RandomSpawnPosition()
    {
        return new Vector3(Random.Range(2.5f, 3), Random.Range(-1f, 3f), 0);
    }

    public void RemoveEnemy(Entity enemy)//enemy die에 넣어 줘야함
    {
        if (enemies.Contains(enemy))
        {
            enemies.Remove(enemy);
        }

        if (enemies.Count == 0)
        {
            CompleteStage();
        }
    }

    public void CompleteStage()
    {
        StageManager.Instance.CompleteStage();
    }

}

