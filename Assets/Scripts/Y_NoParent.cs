using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Y_NoParent : MonoBehaviour
{
    private OVRGrabbable ovrGrabbable;
    private Rigidbody rigid;
    void Start()
    {
        ovrGrabbable = GetComponent<OVRGrabbable>();
         rigid = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        RemoveParent();
    }

    void RemoveParent(){
        if(ovrGrabbable.isGrabbed){
            if(transform.parent){
                    transform.SetParent(null);
                    rigid.isKinematic = false;
            }
            else{
                return;
            }
        }
    }
}
