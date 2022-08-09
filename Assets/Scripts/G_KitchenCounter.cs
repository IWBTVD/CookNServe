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
            int trayNum = collision.gameObject.GetComponent<G_OrderPaper>().orderNumber;
            PhotonNetwork.Instantiate("Tray", trayTransforms[trayNum].position, trayTransforms[trayNum].rotation);
        }
            
    }
}
