﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class MoveAgent : MonoBehaviour
{
    public Transform town;
    
    private readonly float idleSpeed = 1.5f;
    private readonly float traceSpeed = 4.0f;

    private NavMeshAgent agent;
    private EnemyMeleeAI emAI;
    private EnemyRangeAI erAI;
    private FriendManager frAI;
    private bool _ideling;
    public bool ideling
    {
        get { return _ideling; }
        set
        {
            _ideling = value;
            if (_ideling)
            {
                agent.speed = idleSpeed;
                MoveTown();
            }
        }
    }

    private Vector3 _traceTarget;
    public Vector3 traceTarget
    {
        get { return _traceTarget; }
        set
        {
            _traceTarget = value;
            agent.speed = traceSpeed;
            TraceTarget(_traceTarget);
        }
    }

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        emAI = GetComponent<EnemyMeleeAI>();
        erAI = GetComponent<EnemyRangeAI>();
        frAI = GetComponent<FriendManager>();
        agent.autoBraking = false;
        agent.speed = idleSpeed;
        town = GameObject.Find("DefenseTerrain").transform.GetChild(11);
    }

    void MoveTown()
    {
        if (agent.isPathStale) return;
        agent.destination = town.position;
        agent.isStopped = false;
    }

    void Update()
    {
        if(emAI != null)
        {
            if (agent.remainingDistance > emAI.attackDist)
            {
                TraceTarget(_traceTarget);
            }
            if (Vector3.Distance(transform.position, traceTarget) < emAI.attackDist)
            {
                Stop();
                emAI.state = EnemyMeleeAI.STATE.ATTACK;
            }
        }
        if (erAI != null)
        {
            if (agent.remainingDistance > erAI.attackDist)
            {
                TraceTarget(_traceTarget);
            }
            if(Vector3.Distance(transform.position, traceTarget) < erAI.attackDist)
            {
                Stop();
                erAI.state = EnemyRangeAI.STATE.ATTACK;
            }
        }
        if(frAI != null)
        {
            if (frAI.targetTr)
            {
                if (Vector3.Distance(transform.position, frAI.targetTr.position) < frAI.attackDist)
                {
                    Stop();
                    frAI.fState = FriendManager.STATE.ATTACK;
                }
            }
        }

    }

    void TraceTarget(Vector3 pos)
    {
        if (agent.isPathStale) return;
        agent.destination = pos;
        agent.isStopped = false;
    }

    public void Stop()
    {
        agent.isStopped = true;
        agent.velocity = Vector3.zero;
        _ideling = false;
    }

    public void Damage(int damage)
    {
        if (GetComponent<EnemyMeleeAI>() != null)
        {
            if (GetComponent<EnemyMeleeAI>().enemyHealth > damage)
            {
                GetComponent<EnemyMeleeAI>().enemyHealth -= damage;
            }
            else
            {
                GetComponent<EnemyMeleeAI>().enemyHealth = 0;
            }

        }
        else if (GetComponent<EnemyRangeAI>() != null)
        {
            if (GetComponent<EnemyRangeAI>().enemyHealth > damage)
            {
                GetComponent<EnemyRangeAI>().enemyHealth -= damage;
            }
            else
            {
                GetComponent<EnemyRangeAI>().enemyHealth = 0;
            }
        }
    }
}
