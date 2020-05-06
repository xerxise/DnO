using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveRotation : MonoBehaviour
{
    private float h =0;
    private float v=0;
    private Transform tr;
    private float moveSpeed =10f;
    private float rotSpeed = 80f;

    private Animator animator;
    void Start()
    {
        tr = GetComponent<Transform>();
        animator = GetComponent<Animator>();
    }
    
    void Update()
    {
        move();
    }
    void move()
    {
         h = Input.GetAxis("Horizontal");
         v = Input.GetAxis("Vertical");
         Vector3 moveDir = (Vector3.forward * v) + (Vector3.right * h);
         tr.Translate(moveDir.normalized * Time.deltaTime * moveSpeed,Space.Self);
         float y = Mathf.Atan2(moveDir.x,moveDir.z)* Mathf.Rad2Deg;
         Quaternion rot = Quaternion.Euler(0,y,0);
         tr.GetChild(0).rotation = Quaternion.Slerp(tr.GetChild(0).rotation,rot,Time.deltaTime* rotSpeed);
    }
   
}
