using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Y_NoParent : MonoBehaviour
{
    private OVRGrabbable ovrGrabbable;
    void Start()
    {
        ovrGrabbable = GetComponent<OVRGrabbable>();
    }

    // Update is called once per frame
    void Update()
    {
        //RemoveParent();
    }

    void RemoveParent(){
        if(ovrGrabbable.isGrabbed){
            if(transform.parent !=null){
                    transform.parent = null;
            } 
        }
    }
}
