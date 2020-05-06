using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMeleeAttack : MonoBehaviour
{
    private GameObject go;

    private Transform playerTr;
    private Transform enemyTr;

    private MoveAgent agent;

    private float nextFire = 0.0f;
    private readonly float fireRate = 0.5f;
    private readonly float damping = 10.0f;
    public bool isFire = false;

    void Start()
    {
        playerTr = GameObject.FindGameObjectWithTag("PLAYER").GetComponent<Transform>();
        enemyTr = GetComponent<Transform>();
        agent = GetComponent<MoveAgent>();
    }

    void Update()
    {
        if (isFire)
        {
            if (Time.time >= nextFire)
            {
                StartCoroutine(Fire());
                nextFire = Time.time + fireRate + Random.Range(1.0f, 1.5f);
            }

            Quaternion rot = Quaternion.LookRotation(agent.traceTarget - enemyTr.position);
            enemyTr.rotation = Quaternion.Slerp(enemyTr.rotation, rot, Time.deltaTime * damping);
        }
    }

    IEnumerator Fire()
    {
        yield return null;
    }
}
