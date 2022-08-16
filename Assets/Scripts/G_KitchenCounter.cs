using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class G_KitchenCounter : MonoBehaviourPun
{
    public Transform[] trayTransforms;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "OrderPaper")
        {
            //주문서의 주문번호에 맞는 트레이 생성
            int trayNum = collision.gameObject.GetComponent<G_OrderPaper>().orderNumber;
            G_Tray tray = PhotonNetwork.Instantiate("Tray", trayTransforms[trayNum-1].position, trayTransforms[trayNum-1].rotation).GetComponent<G_Tray>();
            tray.SetTrayNumber(trayNum);

            collision.gameObject.GetComponent<G_OrderPaper>().DestroyOrderPaper();
        }
            
    }
}
