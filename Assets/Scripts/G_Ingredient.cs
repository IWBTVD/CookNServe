using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class G_Ingredient : MonoBehaviourPun
{
    
    public IngredientType ingredientType;
    //public bool isUsed = false;

    public float height;
    public bool isUsed = false;
    public bool isCooked = false;

    public GameObject meshObject;

    private MeshCollider triggerCollider;
    private G_PhotonGrabbable grabbable;
    private G_SafeDestroy safeDestroy;
    private Rigidbody rigid;

    private float timer;
    private float stackableTime = 0.5f;
    private bool isStackable = false;
    private bool isGrabbed = false;   //grabbable.isGrabbed보다 한프레임 늦게 켜지고 꺼짐

    public bool isPacked = true;

    private void Start()
    {
        //triggerCollider = GetComponent<MeshCollider>();
        grabbable = GetComponent<G_PhotonGrabbable>();
        safeDestroy = GetComponent<G_SafeDestroy>();
        rigid = GetComponent<Rigidbody>();
    }

    private void Update()
    {

        if (grabbable.isGrabbed)
        {
            isStackable = false;
            isGrabbed = true;

            if (transform.parent)
            {
                G_UnpackChildren unpackChildren = transform.parent.GetComponent<G_UnpackChildren>();
                if (unpackChildren)
                {
                    unpackChildren.UnpackChildren();

                }
            }
        }
        //else if (!isPacked)
            //rigid.isKinematic = false;
            

        //잡는 즉시 한번만 실행
        if (grabbable.isGrabbed != isGrabbed)
        {
            photonView.RPC(nameof(SetTimer), RpcTarget.All);
        }

        //손에서 놓은 후 0.5초동안 그릇에 닿으면 쌓아짐
        if(isStackable)
        {
            timer += Time.deltaTime;
            if (timer >= stackableTime)
            {
                isStackable = false;
            }   
        }
    }

    /// <summary>
    /// 트리거에 햄버거 접시가 닿으면 쌓인다
    /// </summary>
    /// <param name="other">햄버거 접시 충돌체</param>
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Hamburger" && isStackable && !isUsed)
        {
            //if(grabbable.m_grabbedBy)
                //grabbable.m_grabbedBy.ForceRelease(grabbable);
            StackHamburger(other.gameObject);
        }
    }
    private void StackHamburger(GameObject other)
    {

        G_Hamburger hamburger = other.GetComponent<G_Hamburger>();
        hamburger.StackIngredient(meshObject, height, ingredientType);

        isUsed = true;
        //if (safeDestroy)
        //safeDestroy.destroyThis();
        Destroy(gameObject);
    }

    [PunRPC] void SetTimer()
    {
        isGrabbed = false;
        isStackable = true;
        timer = 0f;
    }

}
