using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class G_CustomerGroup : MonoBehaviourPun
{
    public G_CustomerAI.State groupState;

    public G_CustomerAI[] customers;

    public float stateTimer = 0f;

    public float satisfaction = 120f;

    private void Start()
    {
        groupState = customers[0].state;
    }

    private void Update()
    {
        if(stateTimer >= 0)
        {
            stateTimer -= Time.deltaTime;
        }
    }

    //리더가 테이블에 도착하면 나머지 고객도 앉음
    public void SitOnTheSeat(G_Seat seat)
    {
        foreach (G_CustomerAI c in customers)
        {
            if (!c.isLeader)
                c.SitAfterLeader(seat);
        }
    }

    //주문 시작
    [PunRPC]
    public void DoOrder()
    {
        groupState = G_CustomerAI.State.ChoosingMenu;
        //타이머 설정
        stateTimer = Random.Range(5f, 10f);
    }
    

}
