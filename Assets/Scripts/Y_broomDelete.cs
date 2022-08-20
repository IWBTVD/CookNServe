using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Y_BroomDelete : MonoBehaviour
{
    public float timer = 0;

    private G_PhotonGrabbable grabbable;
    private Rigidbody rigid;
    public Transform originalTransform;

    bool first = true;
    public bool isAbandoned = false;    //땅에 떨어졌는가

    private void Start() {
        grabbable = GetComponent<G_PhotonGrabbable>();
        rigid = GetComponent<Rigidbody>();
    }
    void Update()
    {
        if (grabbable.isGrabbed && first)
            {
                //transform.parent = null;
                rigid.isKinematic = false;
                first = false;
            }

        if (isAbandoned)
        {
            timer += Time.deltaTime;
            if (timer > 3f)
            {
                isAbandoned = false;
                transform.position = originalTransform.position;
                transform.rotation = originalTransform.rotation;
                first = true;
                rigid.isKinematic = true;
                timer = 0;
            }
        }
        else
            timer = 0;
    }

    private void OnCollisionEnter(Collision other)
    {
        if ((other.gameObject.tag == "Wall" || other.gameObject.tag == "KitchenCounter") && !grabbable.isGrabbed)
            isAbandoned = true;

        else if (other.gameObject.tag == "Ingredient" && grabbable.isGrabbed)
        {
            Destroy(other.gameObject);
        }
    }

    private void OnCollisionExit(Collision other) {
        if ((other.gameObject.tag == "Wall" || other.gameObject.tag == "KitchenCounter") && !grabbable.isGrabbed)
        {
            isAbandoned = false;
        }
    }
}
