using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Y_Source : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject source;
    public GameObject sourceOutPos;
    public float sourceSpped;

    private OVRGrabbable ovrGrabbable;

    bool isDown;
    void Start()
    {
        ovrGrabbable = GetComponent<OVRGrabbable>();
    }

    // Update is called once per frame
    void Update()
    {
        if(isDown){
            Shot();
        }
    }
    void GetInput(){
        isDown = OVRInput.Get(OVRInput.Button.One);
    }
    void Shot(){
        GameObject intantSource = Instantiate(source);
        intantSource.transform.position = sourceOutPos.transform.position;
        Rigidbody sourceRigid = intantSource.GetComponent<Rigidbody>();
        sourceRigid.velocity = sourceOutPos.transform.forward * sourceSpped;


    }
    
}
