using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class Y_broomDelete : MonoBehaviour
{
    float timer = 0;
    private OVRGrabbable grabbable;
    private Rigidbody rigid;
    private Transform transform;
    public Transform ObjectTransform;
    bool first = true;
    private void Start() {
        grabbable = GetComponent<G_PhotonGrabbable>();
        rigid = GetComponent<Rigidbody>();
        transform = GetComponent<Transform>();
    }
    void Update()
    {
        if (grabbable.isGrabbed && first)
            {
                //transform.parent = null;
                rigid.isKinematic = false;
                first = false;
            }
    }

    private void OnCollisionEnter(Collision other) {
        if((other.gameObject.tag == "Wall" || other.gameObject.tag == "KitchenCounter") && !grabbable.isGrabbed){
            timer += Time.deltaTime;
            Debug.Log("부딪치는중");
            if(timer > 3f){
                transform.position = ObjectTransform.position;
                timer = 0;
            }
        }
        if(other.gameObject.tag == "Ingredient" && grabbable.isGrabbed){
            Destroy(other.gameObject);
        } 
    }
    private void OnCollisionExit(Collision other) {
        timer = 0;
    }
}
