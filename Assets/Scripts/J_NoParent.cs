using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class J_NoParent : MonoBehaviour
{
    private OVRGrabbable ovrGrabbable;
    public GameObject thisObject;
    void Start()
    {
        ovrGrabbable = GetComponent<OVRGrabbable>();
    }

    // Update is called once per frame
    void Update()
    {
        RemoveParent();
    }

    void RemoveParent(){
        if(ovrGrabbable.isGrabbed){
            if(thisObject.GetComponent<Transform>().parent !=null){
                    thisObject.GetComponent<Transform>().parent = null;
            } 
        }
    }
}
