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
    public void DoOrder()
    {
        if(PhotonNetwork.IsMasterClient)
        {
            G_OrderPaper orderPaper = PhotonNetwork.Instantiate("OrderPaper", orderPaperTransform.position, orderPaperTransform.rotation).GetComponent<G_OrderPaper>();
            //photonView.RPC(nameof(InstantiateOrderPaper), RpcTarget.AllBuffered, seatNumber, gameObject, orderPaper);
            orderPaper.SetOrderNumber(seatNumber, this);
        }
    }

    [PunRPC]
    public void InstantiateOrderPaper(int seatNumber, GameObject s, G_OrderPaper o)
    {
        G_Seat seat = s.GetComponent<G_Seat>();
        G_OrderPaper orderPaper = o.GetComponent<G_OrderPaper>();
        orderPaper.SetOrderNumber(seatNumber, seat);
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
