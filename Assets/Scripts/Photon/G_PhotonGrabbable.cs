using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class G_PhotonGrabbable : OVRGrabbable
{
    //[SerializeField]
    //protected new Collider[] m_grabPoints = null;

    private PhotonView photonView;

    // Start is called before the first frame update
    protected override void Start()
    {
        photonView = GetComponent<PhotonView>();
        base.Start();
    }

    public override void GrabBegin(OVRGrabber hand, Collider grabPoint)
    {
        photonView.RequestOwnership();
        base.GrabBegin(hand, grabPoint);
    }
}
