using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

/// <summary>
/// 무작정 Destroy()함수를 사용하면 OVRGrabber에서 미처 계산중인 Collider가 파괴되어 NullReferenceException을 일으킨다.
/// 때문에 OVRGrabber가 오브젝트를 놓게 한 다음에 안전하게 파괴하는 메소드를 구현하였다.
/// 근데 OVRGrabber에서 널체킹해버려서 쓸모가없네 ㅎ
/// </summary>
public class G_SafeDestroy : MonoBehaviour
{

    //public OVRGrabber myGrabber;
    public G_PhotonGrabbable grabbable;

    public OVRGrabber lastGrabber;

    public void destroyThis()
    {
        grabbable = GetComponent<G_PhotonGrabbable>();

        //this gets the hand that's grabbing it
        //myGrabber = grabbable.m_grabbedBy;

        //this turns off the OVRGrabbable script

        //grabbable.enabled = false;

        //use ForceRelease method in the OVRGrabber to release object
        //lastGrabber.ForceRelease(grabbable);

        //destroy object
        Destroy(this.gameObject);
        //PhotonNetwork.Destroy(this.gameObject);

    }

}