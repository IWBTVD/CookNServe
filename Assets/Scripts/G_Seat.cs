using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class G_Seat : MonoBehaviourPun
{
    public int seatNumber;
    public bool isUsing = false;

    public Transform[] seatTransform;
    public Collider tableEntrance;
    public Transform orderPaperTransform;


    public Transform GetThisTableEntrance()
    {
        return tableEntrance.transform;
    }

    public Transform GetSeat(int seatNumber)
    {
        return seatTransform[seatNumber];
    }

    //주문서 생성
    public void InstantiateOrderPaper()
    {
        PhotonNetwork.Instantiate("OrderPaper", orderPaperTransform.position, Quaternion.identity);
    }
}
