using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveRotation1 : MonoBehaviour
{
    public enum State{Idle,Walk,Attack,Run,Damage,Die}
    public enum ActionState{Idle,Walk,Attack,Heal,Run,Damage,Die}
    public State state;
    public ActionState actionState;

    private float h,v = 0;
    private float moveSpeed =0f;
    private float rotSpeed = 80f;
    private float NextTime = 0.5f;
    

    private Vector3 axis;
    private Vector3 dir;

    private Transform tr;
    private Transform currentWeapon;
    private FireCtrl fire;
    private Animator animator;

    public bool isAttack=false;
    public bool isRun=false;
    public bool isDie = false;

    private WaitForSeconds wts;

    private float Distance;
    private float crrentDis;
    private float attackPos =1.2f;
    private float nextFire = 0.0f;

    public  GameObject targetPos;
    public  GameObject hitPos;
    public GameObject playerBullet;

    private Zombie1[] zombie;

    public JoyStick joyStick;

    public int initHp = 10;
    public int crrentHp = 10;

    public bool isSurvival = false;

    void Awake()
    {
        tr = transform.GetComponent<Transform>();
        currentWeapon = GameObject.FindGameObjectWithTag("R_hand_container").transform;
        //targetPos = GameObject.FindGameObjectWithTag("Zombie");
        //joyStick = GameObject.FindObjectOfType<JoyStick>();
        
        hitPos = GameObject.Find("HitPostion");
        animator = transform.GetChild(0).GetComponent<Animator>();
        fire = GameObject.FindObjectOfType<FireCtrl>();
        state = State.Idle; 
        actionState = ActionState.Idle;
        crrentHp = initHp;//Crrent Hp in it Hp 
    }
    //Find Zombie Method
    // void FindZombie()
    // {
    //     for (int i = 0; i < zombie.Length; i++)
    //     {
    //         zombie = GameObject.FindObjectsOfType<Zombie1>();
    //         GameObject a = zombie[i];
    //     }
    // }
    
    void Start()
    {
        wts = new WaitForSeconds(1f);//Attack Time Delay
        StartCoroutine(WaitButton());//Attack Coroutine
    }
    //switch 한번 인식후 유지 계속 입력값이 들어가지 않음
    void MyStateSwitch()
    {
        switch(state)
        {
            case State.Idle:
                animator.SetInteger("Condition",0);
                break;
            case State.Walk:
                animator.SetInteger("Condition",1);
                state = State.Idle;
                break;
            case State.Attack:
                animator.SetTrigger("Attack");
                state =State.Idle;
                break;
            case State.Run:
                animator.SetInteger("Condition",2);
                state = State.Idle;      
                break;
            case State.Damage:
                animator.SetTrigger("Damage");
                state = State.Idle;
                break;
            case State.Die:
                animator.SetInteger("Condition",4);
                break;   
        }
    }
    void ActionStateSwitch()
    {
        switch (actionState)
        {
            case ActionState.Idle:
            //아이들 상태에서 피가 찬다 
            //아디들 상태에서 치료가 된다 
            break;
            case ActionState.Walk:
            break;
            case ActionState.Attack:
            //1번 어택을 할때 피격값이 10이다
            //2번 어택을 할때 피격밧이 20이다
            //zombie.TakeDamage(30);
            //피가 난다   
            break;
            case ActionState.Heal:
            //Heal을 받을때 빨간 빛이 난다; 
            break;
            case ActionState.Run:
            break;
            case ActionState.Damage:
            //데미지를 받을 때 피가 10;
            //데미지를 받을 때 피가 20;
            //데미지를 받을 움직일 수가 없다;
            break;
            case ActionState.Die:
            //죽으면 불활 한다 
            //게임을 끝낸다 
            break;
        }
    }
    void Update()
    {
        JoyStickCtrl();
        ACtrl();
        MyStateSwitch();
        ActionStateSwitch();
    }
   
    //조이스틱 조작 
    private void JoyStickCtrl()
    {
        dir = new Vector3(joyStick.JoystickMove().normalized.x,0,joyStick.JoystickMove().normalized.y);
        if(isRun==false)
        {
            if(isAttack==false)
            {
                if(dir.x != 0 || dir.z != 0)
                {
                actionState = ActionState.Walk;
                state = State.Walk;
                WalkSelect(3f);     
                }
                else if(dir.x == 0 &&dir.z == 0)
                {
                state = State.Idle;
                actionState = ActionState.Idle;
                }
            }
            else return;
        }else Run();  
    }
    //Move Method
    private void WalkSelect(float moveSpeed)
    {
        tr.Translate(tr.transform.forward*Time.deltaTime * moveSpeed ,Space.World);
        Rotation();
    }
    void DIR(float moveSpeed)
    {
        if (dir.x != 0 || dir.z != 0)
        {
            tr.Translate(tr.transform.forward * Time.deltaTime * moveSpeed, Space.World);
            Rotation();
        }
        else return;
    }
    //UI ButtonCtrl
     public void BCtrlDown(string type)
    {
        switch(type)
        {
            case "A":
            isAttack=true;
            break;
            case "B":
            isRun = true;
            break;
        }
    }
    //UI ButtonCtrl
    public void BCtrlUp(string type)
    {
        switch(type)
        {
            case "A":
            isAttack =false;
            break;
            case "B":
            isRun = false;
            break;    
        }
    }
    //Run Swich 
    private void Run()
    {
        if (isRun == true)
        {
            DIR(6f);
            state = State.Run;
        }
        else return;
    }
    //Rotation Method
    private void Rotation()
    {
        float y = Mathf.Atan2(dir.x,dir.z)* Mathf.Rad2Deg;
        Quaternion rot = Quaternion.Euler(0,y,0);
        tr.rotation =
        Quaternion.Slerp(tr.rotation,rot,Time.deltaTime* rotSpeed);
    }
    //Attack Coroutien
    private IEnumerator WaitButton()
    {
        while(isAttack==true)
        {
            isAttack= true;
            yield return wts;
            isAttack=false;
        }
    }
    //Attack Switch
    private void ACtrl()
    {
        if (isAttack == true)
        {
            if (currentWeapon.GetChild(0).CompareTag("PlayerMeleeWeapon"))
            {
                state = State.Attack;
            }
            else if (currentWeapon.GetChild(0).CompareTag("PlayerRangeWeapon"))
            {
                if (Time.time >= nextFire)
                {
                    StartCoroutine(PlayerFire());
                    nextFire = Time.time + 1.0f;
                }
            }
            isAttack = false;
        }
        else return;
    }

    IEnumerator PlayerFire()
    {
        float time = 0.0f;
        List<Transform> list = new List<Transform>();
        if (isSurvival == true) list = GameObject.Find("GraveSpawnManager").GetComponent<GraveSpawnManager>().enemyList;
        else if(isSurvival == false) list = GameObject.Find("SpawnManager").GetComponent<SpawnManager>().enemyList;
        GameObject go = Instantiate(playerBullet, currentWeapon.position, transform.GetChild(0).rotation);
        while (go != null)
        {
            time += Time.deltaTime * 2.0f;
            go.transform.Translate(Vector3.forward * Time.deltaTime * 5.0f);
            if(time >= 10.0f)
            {
                Destroy(go);
                break;
            }
            for (int i = 0; i < list.Count; i++)
            {
                if (Vector3.Distance(go.transform.position, list[i].position) < 1.5f)
                {
                    Transform target = list[i];
                    target.GetComponent<MoveAgent>().Damage(1);
                    Destroy(go);
                    break;
                }
            }
            yield return null;
        }
    }

    //Character Damage Method
    public int MyHpMinus(int Damage)
    {
        crrentHp -= Damage;
        state = State.Damage;
        actionState = ActionState.Damage;
        return crrentHp;
    }
    //Character Heal Method
    public int MyHpPlus(int Heal)
    {
        crrentHp += Heal;
        actionState = ActionState.Heal;
        return crrentHp;
    }
    //Character Die Method
    private void IsDie(int crrentHp)
    {
        if(crrentHp <=0.0f)
        {
            state = State.Die;
            actionState = ActionState.Die;
        }
    }
}
