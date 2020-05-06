using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMeleeAI : MonoBehaviour
{
    public enum STATE
    {
        IDLE,
        TRACE,
        ATTACK,
        DIE
    }

    public STATE state = STATE.IDLE;

    private Transform playerTr;
    private Transform bListTr;
    private List<Transform> barricadeList;
    private Transform enemyTr;
    Transform compare;

    public float attackDist = 3.0f;
    public bool isDie = false;

    private WaitForSeconds wSecond;
    private MoveAgent moveAgent;
    private EnemyMeleeAttack emAttack;
    private BuildManager bManager;
    private SpawnManager sManager;
    private GraveSpawnManager gsManager;

    public int enemyHealth = 10;
    public int erNum;

    private void Awake()
    {
        var player = GameObject.FindGameObjectWithTag("PLAYER");
        if (player != null) playerTr = player.GetComponent<Transform>();
        enemyTr = GetComponent<Transform>();
        moveAgent = GetComponent<MoveAgent>();
        emAttack = GetComponent<EnemyMeleeAttack>();
        bManager = GameObject.Find("Build").GetComponent<BuildManager>();
        if (GameObject.Find("SpawnManager") != null) sManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        if (GameObject.Find("GraveSpawnManager") != null) gsManager = GameObject.Find("GraveSpawnManager").GetComponent<GraveSpawnManager>();

        wSecond = new WaitForSeconds(0.2f);
    }

    void Update()
    {
        if (enemyHealth <= 0)
        {
            if (playerTr.GetComponent<MoveRotation1>().isSurvival == false)
            {
                sManager.enemyMeleeList.RemoveAt(erNum);
                sManager.DestroyedMeleeEnemy(erNum);
            }
            else
            {
                gsManager.enemyList.RemoveAt(erNum);
                gsManager.SurvivalDestroyedEnemy(erNum);
            }
            Destroy(gameObject);
        }
    }

    private void OnEnable()
    {
        StartCoroutine(CheckState());
        StartCoroutine(Action());
    }

    IEnumerator CheckState()
    {
        while (!isDie)
        {
            if (state == STATE.DIE) yield break;
            barricadeList = bManager.obstacleList;
            float distance = Vector3.Distance(playerTr.position, enemyTr.position);
            float bcListDistance = 0.0f;
            if (barricadeList.Count > 0)
            {
                if (barricadeList.Count > 1)
                {
                    for (int i = 0; i < barricadeList.Count; i++)
                    {
                        if (i == 0) compare = barricadeList[i];
                        else
                        {
                            if (Vector3.Distance(compare.position, enemyTr.position) > Vector3.Distance(barricadeList[i].position, enemyTr.position))
                            {
                                compare = barricadeList[i];
                            }
                        }
                        bListTr = compare;
                    }
                }
                else if (barricadeList.Count == 1) bListTr = barricadeList[0];
                bcListDistance = Vector3.Distance(enemyTr.position, bListTr.position);
            }
            if (distance <= attackDist)
            {
                state = STATE.ATTACK;
            }
            else if (bListTr != null && bcListDistance <= attackDist)
            {
                state = STATE.ATTACK;
            }
            else if (playerTr.gameObject != null || bListTr.gameObject != null)
            {
                state = STATE.TRACE;
            }
            else
            {
                state = STATE.IDLE;
            }
            yield return wSecond;
        }
    }

    IEnumerator Action()
    {
        while (!isDie)
        {
            yield return wSecond;
            barricadeList = bManager.obstacleList;
            float distance = Vector3.Distance(playerTr.position, enemyTr.position);
            float bcListDistance = 0.0f;
            if (barricadeList.Count > 0)
            {
                if (barricadeList.Count > 1)
                {
                    for (int i = 0; i < barricadeList.Count; i++)
                    {
                        if (i == 0) compare = barricadeList[i];
                        else
                        {
                            if (Vector3.Distance(compare.position, enemyTr.position) > Vector3.Distance(barricadeList[i].position, enemyTr.position))
                            {
                                compare = barricadeList[i];
                            }
                        }
                        bListTr = compare;
                    }
                }
                else if (barricadeList.Count == 1) bListTr = barricadeList[0];
                bcListDistance = Vector3.Distance(enemyTr.position, bListTr.position);
            }
            switch (state)
            {
                case STATE.IDLE:
                    emAttack.isFire = false;
                    moveAgent.ideling = true;
                    break;
                case STATE.TRACE:
                    emAttack.isFire = false;
                    if (distance < bcListDistance || bListTr == null) moveAgent.traceTarget = playerTr.position;
                    else if (bcListDistance < distance) moveAgent.traceTarget = bListTr.position;
                    break;
                case STATE.ATTACK:
                    moveAgent.Stop();
                    if (emAttack.isFire == false) emAttack.isFire = true;
                    if (bListTr == null) moveAgent.traceTarget = playerTr.position;
                    if (bListTr != null && moveAgent.traceTarget != bListTr.position && moveAgent.traceTarget != playerTr.position) moveAgent.traceTarget = playerTr.position;
                    if (bListTr != null && moveAgent.traceTarget == bListTr.position && distance <= attackDist) moveAgent.traceTarget = playerTr.position;
                    if (bListTr != null && moveAgent.traceTarget == playerTr.position && bcListDistance < distance && distance > attackDist) moveAgent.traceTarget = bListTr.position;
                    if (bListTr != null && bListTr.GetComponent<RepairManager>().wallHealth <= 0) state = STATE.TRACE;
                    break;
                case STATE.DIE:
                    moveAgent.Stop();
                    break;
            }
        }
    }
}
