using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class G_OrderPaper : MonoBehaviourPun
{
    public int orderNumber;

    public TextMeshProUGUI orderNumberText;
    public G_Seat mySeat;

    void Awake()
    {
        //orderNumberText = GetComponentInChildren<TextMeshPro>();
    }

    public void SetOrderNumber(int n, G_Seat seat)
    {
        orderNumber = n;
        orderNumberText.text = orderNumber.ToString();
        mySeat = seat;
    }

    public void DestroyOrderPaper()
    {
        GetComponent<G_SafeDestroy>().destroyThis();
    }

    private void OnCollisionEnter(Collision collision)
    {
        //카운터에 닿았으면
        if (collision.gameObject.tag == "KitchenCounter")
        {
            Transform[] trayTransforms = collision.transform.GetComponent<G_KitchenCounter>().trayTransforms;

            //주문서의 주문번호에 맞는 트레이 생성
            //int trayNum = collision.gameObject.GetComponent<G_OrderPaper>().orderNumber;
            if (PhotonNetwork.IsMasterClient)
            {
                G_Tray tray = PhotonNetwork.Instantiate("Tray", trayTransforms[orderNumber - 1].position, trayTransforms[orderNumber - 1].rotation).GetComponent<G_Tray>();
                //photonView.RPC(nameof(SpawnTray), RpcTarget.AllBuffered, trayTransforms, tray);
                tray.SetTrayNumber(orderNumber, mySeat);
                mySeat.TakeOrderPaper();
            }

            Destroy(gameObject);
        }

    }

    [PunRPC]
    public void SpawnTray(Transform[] trayTransforms, G_Tray tray)
    {
        tray.SetTrayNumber(orderNumber, mySeat);
        mySeat.TakeOrderPaper();
    }
}
