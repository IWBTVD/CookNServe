using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class G_ColaDispensor : MonoBehaviourPun
{
    public Transform colaSpawnTransform;

    // Start is called before the first frame update
    void Start()
    {
        //PhotonNetwork.Instantiate("Cola", colaSpawnTransform.position, colaSpawnTransform.rotation);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
