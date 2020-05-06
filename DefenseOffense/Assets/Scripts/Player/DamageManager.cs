using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageManager : MonoBehaviour
{
    private bool isAttack;
    private bool isSurvival;
    private List<Transform> enList;

    private Transform target;
    
    void Start()
    {
        
    }

    void Update()
    {
        isSurvival = GameObject.FindGameObjectWithTag("PLAYER").GetComponent<MoveRotation1>().isSurvival;
        if (gameObject.CompareTag("PlayerWeapon") )
        {
            isAttack = GameObject.FindGameObjectWithTag("PLAYER").GetComponent<MoveRotation1>().isAttack;
            if (isAttack == true && isSurvival == false)
            {
                enList = GameObject.Find("SpawnManager").GetComponent<SpawnManager>().enemyList;
                for (int i = 0; i < enList.Count; i++)
                {
                    if (Vector3.Distance(transform.position, enList[i].position) < 1.5f)
                    {
                        target = enList[i];
                        target.GetComponent<MoveAgent>().Damage(1);
                        break;
                    }
                }

            }
            else if (isAttack == true && isSurvival == true)
            {
                enList = GameObject.Find("GraveSpawnManager").GetComponent<GraveSpawnManager>().enemyList;
                for (int i = 0; i < enList.Count; i++)
                {
                    if (Vector3.Distance(transform.position, enList[i].position) < 1.5f)
                    {
                        target = enList[i];
                        target.GetComponent<MoveAgent>().Damage(1);
                        break;
                    }
                }
            }
        }
        else if (gameObject.CompareTag("PlayerBullet"))
        {
            if (isSurvival == false)
            {
                enList = GameObject.Find("SpawnManager").GetComponent<SpawnManager>().enemyList;
                for (int i = 0; i < enList.Count; i++)
                {
                    if (Vector3.Distance(transform.position, enList[i].position) < 1.5f)
                    {
                        target = enList[i];
                        target.GetComponent<MoveAgent>().Damage(1);
                        Destroy(gameObject);
                    }
                }
            }
            else if (isSurvival == true)
            {
                enList = GameObject.Find("GraveSpawnManager").GetComponent<GraveSpawnManager>().enemyList;
                for (int i = 0; i < enList.Count; i++)
                {
                    if (Vector3.Distance(transform.position, enList[i].position) < 1.5f)
                    {
                        target = enList[i];
                        target.GetComponent<MoveAgent>().Damage(1);
                        Destroy(gameObject);
                    }
                }
            }
        }
        else if (gameObject.CompareTag("FriendWeapon"))
        {
            if (isSurvival == false)
            {
                enList = GameObject.Find("SpawnManager").GetComponent<SpawnManager>().enemyList;
                for (int i = 0; i < enList.Count; i++)
                {
                    if (Vector3.Distance(transform.position, enList[i].position) < 1.5f)
                    {
                        target = enList[i];
                        target.GetComponent<MoveAgent>().Damage(1);
                        Destroy(gameObject);
                    }
                }

            }
            else if (isSurvival == true)
            {
                enList = GameObject.Find("GraveSpawnManager").GetComponent<GraveSpawnManager>().enemyList;
                for (int i = 0; i < enList.Count; i++)
                {
                    if (Vector3.Distance(transform.position, enList[i].position) < 1.5f)
                    {
                        target = enList[i];
                        target.GetComponent<MoveAgent>().Damage(1);
                        Destroy(gameObject);
                    }
                }
            }
        }
    }
}
