using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.InputSystem.XR;
using UnityEngine.Rendering;

public class EnemyManager : Singleton<EnemyManager>
{
    [SerializeField] Enemy[] Enemys;
    [SerializeField] GameObject[] CapterMap;

    int stage = 0;
    public int spawncount;

    private List<GameObject> spawnEnemy = new List<GameObject>();
    Enemy enemy;

    public void Start()
    {
        StartCoroutine(CapterCoroutine());
        enemy = FindObjectOfType<Enemy>();
    }

    private IEnumerator CapterCoroutine()
    {
        GameObject tlieMap;
        for (int i = 0; i < CapterMap.Length;)
        {
            tlieMap = Instantiate(CapterMap[i], transform.position, Quaternion.identity);
            Debug.Log("현재 챕터 : " + (i+1)); // UI 표시
            stage = 0;
            if (stage == 0)
            {
                for (int s = stage; s <= 4; s++)
                {
                    Debug.Log("현재 스테이지 : " + (s + 1)); // UI 표시
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
                enemyObject = Instantiate(Enemys[3].gameObject, spawnPosition, Quaternion.identity);
                enemyObject.name = "Boss Enemy";
                spawncount++;
                return;
            }
            else
            {
                enemyObject = Instantiate(Enemys[num].gameObject, spawnPosition, Quaternion.identity);
                enemyObject.name = "Normal Enemy" + i;
                spawncount++;
            }
            spawnEnemy.Add(enemyObject);
        }
    }


    public void NextStage()
    {
        spawncount = 0;
        SpawnEnemy();
        //enemy.GrowthStats();
    }

}

