using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class G_PhotonGrabber : OVRGrabber
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    protected override void DoWhenTriggerEnter(Collider otherCollider)
    {
        // Get the grab trigger
        OVRGrabbable grabbable = otherCollider.GetComponent<OVRGrabbable>() ?? otherCollider.GetComponentInParent<OVRGrabbable>();
        if (grabbable == null)
            grabbable = otherCollider.GetComponent<G_PhotonGrabbable>() ?? otherCollider.GetComponentInParent<G_PhotonGrabbable>();
        if(grabbable == null) return;

        // Add the grabbable
        int refCount = 0;
        m_grabCandidates.TryGetValue(grabbable, out refCount);
        m_grabCandidates[grabbable] = refCount + 1;
    }

    protected override void DoWhenTriggerExit(Collider otherCollider)
    {
        OVRGrabbable grabbable = otherCollider.GetComponent<OVRGrabbable>() ?? otherCollider.GetComponentInParent<OVRGrabbable>();
        if (grabbable == null) grabbable = otherCollider.GetComponent<G_PhotonGrabbable>() ?? otherCollider.GetComponentInParent<G_PhotonGrabbable>();
        if (grabbable == null) return;

        // Remove the grabbable
        int refCount = 0;
        bool found = m_grabCandidates.TryGetValue(grabbable, out refCount);
        if (!found)
        {
            return;
        }

        if (refCount > 1)
        {
            m_grabCandidates[grabbable] = refCount - 1;
        }
        else
        {
            m_grabCandidates.Remove(grabbable);
        }
    }
}
