using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 무작정 Destroy()함수를 사용하면 OVRGrabber에서 미처 계산중인 Collider가 파괴되어 NullReferenceException을 일으킨다.
/// 때문에 OVRGrabber가 오브젝트를 놓게 한 다음에 안전하게 파괴하는 메소드를 구현하였다.
/// </summary>
public class G_SafeDestoy : MonoBehaviour
{

    private OVRGrabber myGrabber;

    public void destroyThis()

    {

        //this turns off the OVRGrabbable script

        this.GetComponent<OVRGrabbable>().enabled = false;

        //this gets the hand that's grabbing it

        myGrabber = this.GetComponent<OVRGrabbable>().m_grabbedBy;

        //use ForceRelease method in the OVRGrabber to release object

        myGrabber.ForceRelease(this.gameObject.GetComponent<OVRGrabbable>());

        //destroy object

        Destroy(this.gameObject);

    }

}
