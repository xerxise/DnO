using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraveSpawnManager : MonoBehaviour
{
    private bool isActive = false;

    private List<Transform> basicSpawn;
    private List<Transform> coroutineSpawn;
    public List<Transform> enemyList;

    private int spawnCount = 3;
    private int count = 0;
    private int coCount = 0;
    public int enemyCount = 0;

    public GameObject survivalEnemy;
    public Transform player;

    void Start()
    {
        basicSpawn = new List<Transform>(); // Transform[GameObject.Find("BasicGrave").transform.childCount];
        coroutineSpawn = new List<Transform>(); // Transform[GameObject.Find("CoroutineGrave").transform.childCount];
        player = GameObject.FindGameObjectWithTag("PLAYER").transform;

        for (int i=0; i< GameObject.Find("BasicGrave").transform.childCount; i++)
        {
            basicSpawn.Add(GameObject.Find("BasicGrave").transform.GetChild(i));
        }
        for (int i = 0; i < GameObject.Find("CoroutineGrave").transform.childCount; i++)
        {
            coroutineSpawn.Add(GameObject.Find("CoroutineGrave").transform.GetChild(i));
        }
        StartCoroutine(GraveSpawn());
    }

    void Update()
    {
        
    }
    
    IEnumerator GraveSpawn()
    {
        
        while (player.GetComponent<MoveRotation1>().isDie == false)
        {
            yield return new WaitForSeconds(7.0f);
            isActive = GameObject.FindGameObjectWithTag("PLAYER").GetComponent<MoveRotation1>().isSurvival;
            List<Transform> bSpawn = basicSpawn;
            List<Transform> cSpawn = coroutineSpawn;
            if (isActive)
            {
                for (int i = 0; i < spawnCount; i++)
                {
                    int rNum = Random.Range(0, basicSpawn.Count);
                    Vector3 sPosition = bSpawn[rNum].position;
                    GameObject go = Instantiate(survivalEnemy, sPosition, Quaternion.identity);
                    enemyList.Add(go.transform);
                    go.GetComponent<EnemyMeleeAI>().erNum = enemyCount;
                    enemyCount++;
                }
                count++;
                if (count > 10)
                {
                    spawnCount++;
                    if (coCount < cSpawn.Count && coCount < 5)
                    {
                        cSpawn[coCount].gameObject.SetActive(true);
                        basicSpawn.Add(cSpawn[coCount]);
                        coCount++;
                    }
                    count = 0;
                }
            }
        }
    }

    public void SurvivalDestroyedEnemy(int index)
    {
        if (enemyList.Count == 0)
        {
            enemyCount = 0;
        }
        else if (enemyList.Count == 1)
        {
            enemyList[0].GetComponent<RepairManager>().listNum = 0;
            enemyCount = 1;
        }
        else
        {
            for (int i = index; i < enemyList.Count; i++)
            {
                enemyList[i].GetComponent<EnemyRangeAI>().erNum -= 1;
                enemyCount = enemyList.Count - 1;
            }
        }
    }
}
