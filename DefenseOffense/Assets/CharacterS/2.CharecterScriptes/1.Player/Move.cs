using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    private float h =0;
    private float v=0;
    private Transform tr;
    private float moveSpeed =10f;
    void Start()
    {
       tr = GetComponent<Transform>();
        
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
    }
}
