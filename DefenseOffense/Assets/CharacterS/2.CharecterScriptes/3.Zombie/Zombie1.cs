using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie1 : MonoBehaviour
{
//     public enum State {Idle,Walk,Attack,Damage,Die}
//     public State state;
//     public float zombieHp =100;
//     public Transform[] points;
// 	public int nextIdx = 0;
// 	private Animator anim;
// 	public float speed = 3.0f;
// 	public float damping = 5.0f;
// 	private bool attack = false;
//     private bool walk = false;
//     private bool damage =false;
//     private bool die = false;
// 	private Vector3 movePos;
//     private Transform tr;
// 	private Transform PlayerTr;
//     private Vector3 hitPos;
//     public MoveRotation1 People;
//     void Awake()
//     {
//         GameObject p = GameObject.Find("WayPointGroup");
//         points = new Transform[p.transform.childCount];
//         for(int i = 0; i < p.transform.childCount; i++)
//         {
//             points[i] = p.transform.GetChild(i);
//         }
//         PlayerTr = GameObject.FindGameObjectWithTag("PLAYER").GetComponent<Transform>();
//         People = GameObject.FindObjectOfType<MoveRotation1>();
// 		tr = GetComponent<Transform>();
// 		anim = transform.GetChild(0).GetComponent<Animator>();
//         state = State.Idle;
        
//     }
// 	void Start()
//     {
//     }
//     void Update()
//     {
// 		ZombieAI();
//         ZombieSate();
// 	}
//     void ZombieSate()
//     {
//         switch(state)
//         {
//             case State.Idle:
//             anim.SetInteger("Condition",0);
//             break;
//             case State.Walk:
//             anim.SetInteger("Condition",1);
//             state = State.Idle;
//             break;
//             case State.Attack:
//             anim.SetTrigger("Attack");
//             state = State.Idle;
//             People.MyHp(10);
//             Debug.Log("Attack");
//             break;
//             case State.Damage:
//             anim.SetTrigger("Damage");
//             state = State.Idle;
//             break;
//             case State.Die:
//             anim.SetInteger("Condition",4);
//             break;
//         }
//     }
//     void ZombieAI()
//     {
//         if( die == false)
//         {
//             if(damage==false)
//             {
//                 float dist = Vector3.Distance(tr.position, PlayerTr.position);
//                 if (dist <= 1.0f)
//                 {   
//                     StartCoroutine(WaitAttack());
//                     if(attack==true)
//                     {
                        
//                         Debug.Log("10");
//                     } 
//                 }
//                 else if(dist <= 6.0f)
//                 {
//                     movePos = PlayerTr.position;
//                     attack =false;
//                 }
//                 else 
//                 {
//                     movePos =points[nextIdx].position;
                    
//                 }
//                 if(attack==false)
//                 {
//                     Debug.Log("false");
//                     tr.Translate(Vector3.forward *Time.deltaTime *speed);
//                     state = State.Walk; 
//                     Quaternion ro = Quaternion.LookRotation(movePos - tr.position);
//                     tr.rotation = Quaternion.Slerp(tr.rotation, ro, Time.deltaTime * damping);
//                     state = State.Walk; 
                    
//                     // tr.position = Vector3.MoveTowards(tr.position, points[nextIdx].position, Time.deltaTime * 3f);
//                     // Vector3 dir = points[nextIdx].position - tr.position;
//                     // Quaternion rot = Quaternion.LookRotation(dir);
//                     // tr.rotation = Quaternion.Slerp(tr.rotation, rot, Time.deltaTime * 6f);
//                     // state = State.Walk; 
//                 }
//                 if (Vector3.Distance(points[nextIdx].position,tr.position) < 0.1f)
//                 {
//                     nextIdx++;
//                     if (nextIdx >= points.Length)
//                     {
//                         nextIdx = 0;
//                     }
//                 }
//             }
//         }    
       
//     }
//     IEnumerator WaitAttack()
//     {   
//         attack = true;
//         state =State.Attack;
//         Debug.Log("true");
//         yield return new WaitForSeconds(10f);
//         attack = false;
//         yield return new WaitForSeconds(10f);

//     }
//     public void TakeDamage(int damage)
//     {
//         zombieHp -= damage;
//         if(damage==10)
//         state = State.Damage;
//         if(zombieHp <=0)
//         {
//             Debug.Log("좀비다이");
//             state = State.Die;
//         }
//     }
}
