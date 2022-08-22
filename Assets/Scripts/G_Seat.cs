using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class G_Seat : MonoBehaviourPun
{
    public int seatNumber;
    public bool isUsing = false;

    public Transform[] seatTransforms;
    public Transform[] tableTransforms;
    public Collider tableEntrance;
    public Transform orderPaperTransform;

    public G_CustomerGroup myGroup;


    public Transform GetThisTableEntrance()
    {
        return tableEntrance.transform;
    }

    public Transform GetSeat(int seatNumber)
    {
        return seatTransforms[seatNumber];
    }

    //주문서 생성
    public void InstantiateOrderPaper()
    {
        G_OrderPaper orderPaper = PhotonNetwork.Instantiate("OrderPaper", orderPaperTransform.position, orderPaperTransform.rotation).GetComponent<G_OrderPaper>();
        //주문서 변수 할당
        orderPaper.SetOrderNumber(seatNumber, this);
    }

    public void TakeOrderPaper()
    {
        myGroup.WaitForMeal();
    }

    public void ServedFood(GameObject trayObject)
    {
        if(PhotonNetwork.IsMasterClient)
        {
            foreach (Transform t in tableTransforms)
            {
                Instantiate(trayObject, t.position, t.rotation);
                myGroup.ServedFood();
            }
        }
    }

    public void SetGroup(G_CustomerGroup g)
    {
        myGroup = g;
        //photonView.RPC(nameof(SetGroupRPC), RpcTarget.AllBuffered, g);
    }

    //[PunRPC]
    //public void SetGroupRPC(G_CustomerGroup g)
    //{
    //    myGroup = g;
    //}
}
