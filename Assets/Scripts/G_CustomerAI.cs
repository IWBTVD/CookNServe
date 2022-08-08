using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class G_CustomerAI : MonoBehaviour
{
    public enum State
    {
        Idle,
        HeadingToSeat,
        ChoosingMenu,
        ReadyToOrder,
        WaitingForMeal,
        Eating,
        HeadingToPay,
        Leaving,
    }

    public State state = State.Idle;

    public bool isLeader = false;
    public int myNumberInGroup = 0; //손님 무리에서 내가 몇번째인지 표시
    public GameManager gameManager;

    private NavMeshAgent nav;
    private Transform destination;
    private Animator anim;
    private CapsuleCollider capsuleCollider;

    private G_Seat mySeat;
    public G_CustomerGroup myGroup;

    private bool isLeaderSit = false;
    private bool isStateDone = false;
    public float stateTimer;   //time.deltatime을 빼서 행동 완료시간을 검사함

    private void Awake()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        nav = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        capsuleCollider = GetComponent<CapsuleCollider>();
        myGroup = GetComponentInParent<G_CustomerGroup>();
    }

    void Start()
    {
        state = State.HeadingToSeat;

        //그룹 리더면 자리를 찾아서 그쪽으로 출발함
        if (isLeader)
        {
            mySeat = gameManager.FindSeat(isLeader);
            destination = mySeat.transform;
        }
            
        //그룹 리더가 아니면 리더를 따라감
        else
        {
            destination = myGroup.customers[0].transform;
        }
            

        anim.SetBool("isWalk", true);
        nav.SetDestination(destination.position);
    }

    void Update()
    {
        switch(state)
        {
            case State.HeadingToSeat: HeadToSeat();  break;
            case State.ChoosingMenu: ChooseMenu();  break;
            case State.ReadyToOrder: BeReadyToOrder();  break;
            case State.WaitingForMeal: WaitForMeal();  break;
            case State.Eating: Eat();  break;
            case State.HeadingToPay: HeadToPay();  break;
            case State.Leaving: Leaving();  break;
        }
        if(stateTimer > 0)
        {
            stateTimer -= Time.deltaTime;
        }
    }

    /// <summary>
    /// 식당에 입장하여 자리를 찾아가는 상태
    /// </summary>
    private void HeadToSeat()
    {
        //리더가 아니면 리더를 계속 따라간다
        if(!isLeader && nav.isActiveAndEnabled && !isLeaderSit)
        {
            nav.SetDestination(myGroup.customers[0].transform.position);
        }
            
        //테이블에 도착했으면 앉고, 주문한다
        if (isStateDone)
        {
            capsuleCollider.enabled = false;
            nav.radius = 0.01f;
            nav.enabled = false;
            transform.position = mySeat.GetSeat(myNumberInGroup).position;
            transform.rotation = mySeat.GetSeat(myNumberInGroup).rotation;
            anim.SetBool("isWalk", false);
            anim.SetBool("isSit", true);
            stateTimer = Random.Range(5f, 10f);
            if(isLeader)
            {
                //내 그룹에게 자리에 앉았다고 호출한다
                myGroup.SitOnTheSeat(mySeat);
            }
            state = State.ChoosingMenu;
        }
        isStateDone = false;
    }

    private void ChooseMenu()
    {
        if(stateTimer <= 0)
        {
            anim.SetBool("isReadyToOrder", true);
            state = State.ReadyToOrder;
            stateTimer = 0f;
        }
    }
    
    private void BeReadyToOrder()
    {

    }

    private void WaitForMeal()
    {

    }

    private void Eat()
    {

    }

    private void HeadToPay()
    {

    }

    private void Leaving()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "TableEntrance" && state == State.HeadingToSeat)
        {
            isStateDone = true;
        }
    }

    //리더가 자리에 앉으면 같은 자리로 앉는다
    public void SitAfterLeader(G_Seat seat)
    {
        isLeaderSit = true;
        destination = seat.GetThisTableEntrance();
        nav.SetDestination(destination.position);

        mySeat = seat;
    }
}
