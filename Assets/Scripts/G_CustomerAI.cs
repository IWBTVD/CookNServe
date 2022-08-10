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
    public int myNumberInGroup = 0; //손님 무리에서 내가 몇번째인지 표시, 내가 앉을 좌석 번호랑 연관
    public GameManager gameManager;

    private NavMeshAgent nav;
    private Transform destination;
    private Animator anim;
    private CapsuleCollider capsuleCollider;
    private Rigidbody rigid;

    private G_Seat mySeat;  //내 좌석 스크립트
    public G_CustomerGroup myGroup; //내 그룹 스크립트

    private bool isLeaderSit = false;
    private bool isStateDone = false;

    private void Awake()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        nav = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        capsuleCollider = GetComponent<CapsuleCollider>();
        rigid = GetComponent<Rigidbody>();
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
            //테이블과 충돌하는거 없애기 위한 작업
            capsuleCollider.enabled = false;
            nav.radius = 0.01f;
            nav.enabled = false;

            //좌석 위치에 텔레포트함
            transform.position = mySeat.GetSeat(myNumberInGroup).position;
            transform.rotation = mySeat.GetSeat(myNumberInGroup).rotation;
            anim.SetBool("isWalk", false);
            anim.SetBool("isSit", true);

            //리더가 앉았으면 그룹에 주문시간 타이머를 작동시킨다
            if(isLeader)
            {
                //내 그룹에게 자리에 앉았다고 호출한다
                myGroup.SitOnTheSeat(mySeat);
                //그룹에 주문 타이머를 동작시킨다
                myGroup.photonView.RPC("DoOrder", Photon.Pun.RpcTarget.AllBuffered);
            }
            rigid.isKinematic = true;
            state = State.ChoosingMenu;
        }
        isStateDone = false;
    }

    //customerGroup의 타이머가 소진될때까지 기다림
    public void ChooseMenu()
    {
        if (myGroup.stateTimer <= 0)
        {
            //다 소진되면 주문 준비함
            anim.SetBool("isReadyToOrder", true);
            state = State.ReadyToOrder;
            //리더면 주문서 생성
            if(isLeader)
                mySeat.InstantiateOrderPaper();
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
