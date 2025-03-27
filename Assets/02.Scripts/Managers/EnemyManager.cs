using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.InputSystem.XR;

public class EnemyManager : Singleton<EnemyManager>
{
    [SerializeField] Enemy[] Enemys;

    int stage = 1;
    int spawncount = 0;


    public void Start()
    {
        StartCoroutine(StageCoroutine());
    }

    private IEnumerator StageCoroutine()
    {
        for (int i = stage; i < 15;)
        {
            while (spawncount == 0)
            {
                Debug.Log("Stage: " + stage);
                SpawnEnemy();
            }
            yield return new WaitUntil(() => GameObject.FindGameObjectsWithTag("Enemy").Length == 0);
            i++;
            yield return new WaitForSeconds(2f);
        }
    }
    public void SpawnEnemy()
    {
        int enmys = 5;
        for (int i = 0; i < enmys ; i++)
        {
            Vector3 spawnPosition = new Vector3(Random.Range(2.5f, 3), Random.Range(1f, 3f), Random.Range(0f, 10f));
            int num = Random.Range(0, 2);
            if (stage % 3 == 0 && stage != 0)
            {
                //GameObject bossObject = Instantiate(Enemys[3], spawnPosition, Quaternion.identity);
                spawncount++;
            }
            else
            {
                //GameObject enemyObject = Instantiate(Enemys[num], spawnPosition, Quaternion.identity);
                spawncount++;
            }
        }
    }
}

