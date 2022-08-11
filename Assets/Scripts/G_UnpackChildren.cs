using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class G_UnpackChildren : MonoBehaviourPun
{
    private G_PhotonGrabbable grabbable;

    public GameObject[] ingredients;

    private void Start()
    {
        grabbable = GetComponent<G_PhotonGrabbable>();
    }

    private void Update()
    {

    }

    [PunRPC]
    public void UnpackIngredient()
    {
        foreach(GameObject i in ingredients)
        {
            i.transform.SetParent(null);
            Rigidbody r = i.GetComponent<Rigidbody>();
            r.isKinematic = false;
            r.useGravity = true;
        }
    }

    public void UnpackChildren()
    {
        photonView.RPC(nameof(UnpackIngredient), RpcTarget.AllBuffered);
    }
}
