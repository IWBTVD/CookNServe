using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;

public class NetworkedPlayer : MonoBehaviourPun
{
    public GameObject ovrCameraRig;
    public GameObject otherModel;

    public OVRPlayerController ovrPlayerController;
    public OVRSceneSampleController ovrSceneSampleController;

    private void Awake()
    {
        ovrPlayerController = GetComponent <OVRPlayerController>();
        ovrSceneSampleController = GetComponent<OVRSceneSampleController>();

        ovrCameraRig.SetActive(photonView.IsMine);
        otherModel.SetActive(!photonView.IsMine);

        if(photonView.IsMine)
        {
            ovrPlayerController.enabled = true;
            ovrSceneSampleController.enabled = true;
        }

    }
}