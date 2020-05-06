using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieManager : MonoBehaviour
{
    private List<GameObject> ZombieList;
    public GameObject myobject;
    public Transform myPos;
    private Vector3 RandomPos;
    
    void Start()
    {
        myobject = GameObject.Find("zombie");
        myPos = GetComponent<Transform>();
        myPos.position = Vector3.zero;
        StartCoroutine(a());
    }

    void InStantiAte()
    {
        myPos.position =
        new Vector3(Random.Range(0,10),0,Random.Range(0,10));
        RandomPos = myPos.position;
        Instantiate(myobject,RandomPos,Quaternion.identity);
    }

    IEnumerator a()
    {
        while(true)
        {
            yield return new WaitForSeconds(3f);
            Debug.Log("생성");
            InStantiAte();
        }
    }
    void Update()
    {

    }
}

