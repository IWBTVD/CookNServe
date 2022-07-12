using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class G_CreatedObject : MonoBehaviour
{
    private Rigidbody rigid;
    private OVRGrabbable grabbable;
    private MeshCollider meshCollider;

    public G_ObjectBundle bundle;

    public bool isNew = true;

    private void Start()
    {
        grabbable = GetComponent<OVRGrabbable>();
        rigid = GetComponent<Rigidbody>();
        meshCollider = GetComponent<MeshCollider>();
        rigid.isKinematic = false;
        rigid.useGravity = false;
        meshCollider.isTrigger = true;

        bundle = GetComponentInParent<G_ObjectBundle>();
        Invoke("SetTrigger", 2f);
    }

    private void Update()
    {
        //잡은 물체가 아직 사용된 적이 없으면
        if (grabbable.isGrabbed && isNew)
        {
            rigid.isKinematic = true;
            rigid.useGravity = true;
            isNew = false;
        }
    }

    private void SetTrigger()
    {
        meshCollider.isTrigger = false;
        if (bundle)
            bundle.GrabbedCreatedObject();
    }
}
