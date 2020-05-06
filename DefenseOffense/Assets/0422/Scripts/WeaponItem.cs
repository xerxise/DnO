using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponItem : MonoBehaviour {

    public Transform playerPos;
    public GameObject sword;

    private void Start()
    {
        playerPos = GameObject.FindGameObjectWithTag("R_hand_container").transform;
    }

    public void Use() {

        if(playerPos.childCount >= 1) return;

        GameObject a = Instantiate(sword, playerPos.position, sword.transform.rotation, playerPos.transform);
        a.transform.localPosition = Vector3.zero;
        a.transform.localRotation = Quaternion.identity;
        a.GetComponent<BoxCollider>().enabled = false;
    }
}
