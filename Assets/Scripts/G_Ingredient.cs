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

    private float timer;
    private float stackableTime = 0.5f;
    private bool isStackable = false;
    private bool isGrabbed = false;   //grabbable.isGrabbed보다 한프레임 늦게 켜지고 꺼짐


    private void Start()
    {
        triggerCollider = GetComponent<MeshCollider>();
        grabbable = GetComponent<G_PhotonGrabbable>();
    }

    private void Update()
    {
        
        if (grabbable.isGrabbed)
        {
            isStackable = false;
            isGrabbed = true;
            triggerCollider.enabled = false;
        }
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
            StackHamburger(other);
        }
    }
    private void StackHamburger(Collider other)
    {
        G_Hamburger hamburger = other.GetComponent<G_Hamburger>();
        hamburger.StackIngredient(meshObject, height, ingredientType);

        isUsed = true;
        meshObject.GetComponent<G_RemovableMeshFilter>().RemoveMeshFilter();
        Destroy(gameObject, 2f);
    }

    [PunRPC] void SetTimer()
    {
        isGrabbed = false;
        isStackable = true;
        timer = 0f;
    }
}
